using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.UniversalConnector.Factory;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Dal
{
    class UniversalConnectorDAL : IUniversalConnectorDAL
    {
        #region Constants

        private const string ExceptionMessageParameterMatchFailure = "The number of parameters does not match number of values for stored procedure.";

        #endregion Constants

        #region Private Member Variables

        private DbConnection _connection = null;
        private DbCommand _command = null;
        private ServerEnumType _server;
        private DbTransaction _transaction = null;
        private object[] _parameters = null;
        private string _connectionString = string.Empty;
        private DbDataAdapter _adapter = null;
        private DbDataReader _dataReader;

        #endregion Private Member Variables

        #region Private Properties

        /// <summary>
        /// Obtiene o establece el objeto DbConnection
        /// </summary>
        private DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = ConnectorFactory.GetConnection(Server);
                    _connection.ConnectionString = ConnectionString;
                }
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el objeto DbCommand
        /// </summary>
        private DbCommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = ConnectorFactory.GetCommand(Server);
                }

                return _command;
            }
            set
            {
                _command = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la transacción DbTransaction en la que se ejecuta en el objeto DbCommand de esta clase.
        /// </summary>
        private DbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                _transaction = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el objeto DbDataAdapter
        /// </summary>
        private DbDataAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    _adapter = ConnectorFactory.GetDataAdapter(Server);

                }
                return _adapter;
            }

            set
            {
                _adapter = value;
            }
        }

        /// <summary>
        /// Obtiene el array de parametros.
        /// </summary>
        private object[] Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adjunta o agrega un array de parametros a un objeto command
        /// </summary>
        /// <param name="command">Objeto commando</param>
        /// <param name="commandParameters">Array de parametros a agregar</param>
        private void AttachParameters(DbCommand command, object[] commandParameters)
        {
            CreateAutomaticDbCommandParameters(command);
            AssignParameters(command, commandParameters);
        }

        /// <summary>
        /// <para>Discovers parameters on the <paramref name="command"/> and assigns the values from <paramref name="parameterValues"/> to the <paramref name="command"/>s Parameters list.</para>
        /// </summary>
        /// <param name="command">The command the parameeter values will be assigned to</param>
        /// <param name="parameterValues">The parameter values that will be assigned to the command.</param>
        private void AssignParameters(DbCommand command, object[] parameterValues)
        {

            if (SameNumberOfParametersAndValues(command, parameterValues) == false)
            {
                throw new InvalidOperationException(ExceptionMessageParameterMatchFailure);
            }

            AssignParameterValues(command, parameterValues);
        }

        /// <summary>
        /// Asigna los valores a los parametros contenidos en el objeto DbCommand
        /// </summary>
        /// <param name="command">Objeto DbCommand</param>
        /// <param name="values">Valores de los parametros</param>
        private void AssignParameterValues(DbCommand command,
                                   object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                DbParameter parameter = command.Parameters[i];

                // There used to be code here that checked to see if the parameter was input or input/output
                // before assigning the value to it. We took it out because of an operational bug with
                // deriving parameters for a stored procedure. It turns out that output parameters are set
                // to input/output after discovery, so any direction checking was unneeded. Should it ever
                // be needed, it should go here, and check that a parameter is input or input/output before
                // assigning a value to it.
                SetParameterValue(command, parameter.ParameterName, values[i]);
            }
        }

        /// <summary>
        /// Sets a parameter value.
        /// </summary>
        /// <param name="command">The command with the parameter.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        private void SetParameterValue(DbCommand command,
                                              string parameterName,
                                              object value)
        {
            if (command == null) throw new ArgumentNullException("command");

            command.Parameters[parameterName].Value = value ?? DBNull.Value;
        }

        /// <summary>
        /// Determines if the number of parameters in the command matches the array of parameter values.
        /// </summary>
        /// <param name="command">The <see cref="DbCommand"/> containing the parameters.</param>
        /// <param name="values">The array of parameter values.</param>
        /// <returns><see langword="true"/> if the number of parameters and values match; otherwise, <see langword="false"/>.</returns>
        protected bool SameNumberOfParametersAndValues(DbCommand command,
                                                               object[] values)
        {
            int numberOfParametersToStoredProcedure = command.Parameters.Count;
            int numberOfValuesProvidedForStoredProcedure = values.Length;
            return numberOfParametersToStoredProcedure == numberOfValuesProvidedForStoredProcedure;
        }

        /// <summary>
        /// Crea automaticamente los parametos a usar en una consulta SQL
        /// </summary>
        /// <param name="command">Objeto DbCommand que se le van a crear los parametros</param>
        private void CreateAutomaticDbCommandParameters(DbCommand command)
        {
            switch (command.CommandType)
            {
                case CommandType.StoredProcedure:
                    Factory.ConnectorFactory.DeriveParameter(Server, command);
                    break;
                case CommandType.Text:
                    CreateNormalDbCommandParameters(command);
                    break;
            }
        }

        /// <summary>
        /// Crea los parametros cuando el objeto DbCommand su propiedad CommandType es Text
        /// </summary>
        /// <param name="command">Objeto DbCommand que se le van a crear los parametros</param>
        private void CreateNormalDbCommandParameters(DbCommand command)
        {
            List<string> parametersNames = GetParameterNames(command.CommandText);
            foreach (string parameterName in parametersNames)
            {
                DbParameter parameter = Factory.ConnectorFactory.GetParameter(Server);
                parameter.ParameterName = parameterName;
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Obtiene los nombres de los parametros de la consulta SQL
        /// </summary>
        /// <param name="commandText">Consulta SQL</param>
        /// <returns>Lista de nombres de parametros</returns>
        private List<string> GetParameterNames(string commandText)
        {
            string sPattern = @"(?<!@)@\w{1,}";

            return Regex.Matches(commandText, sPattern, RegexOptions.IgnoreCase).Cast<Match>().Select(m => m.Value).ToList();
        }

        /// <summary>
        /// Prepara el objeto command estableciendo sus diferentes propiedades.
        /// </summary>
        /// <param name="command">Objeto command a preparar</param>
        /// <param name="connection">Conexión a utilizar</param>
        /// <param name="transaction">Transacción a establacer</param>
        /// <param name="commandType">Tipo de comando a utilizar</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="commandParameters">Parametros a utilizar</param>
        private void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, object[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        /// <summary>
        /// Cierra el objeto DbDataReader.
        /// </summary>
        private void CloseReader()
        {
            if (DataReader != null)
            {
                DataReader.Close();
                DataReader.Dispose();
            }
        }

        /// <summary>
        /// Rellena los DbCommands (INSERT, UPDATE, DELETE) del Adaptador
        /// </summary>
        /// <param name="adapter">Adaptador a llenar</param>
        private void FillAdapterCommands(DbDataAdapter adapter)
        {
            using (DbDataAdapter auxAdapter = ConnectorFactory.GetDataAdapter(Server))
            {
                auxAdapter.SelectCommand = adapter.SelectCommand;
                using (DbCommandBuilder commandBuilder = ConnectorFactory.GetCommandBuilder(Server, auxAdapter))
                {
                    adapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);
                    adapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
                    adapter.InsertCommand = commandBuilder.GetInsertCommand(true);
                    SetupTransactionAdapterCommands(adapter);
                }
            }
        }

        /// <summary>
        /// Si se usa transaccion, se le asigna la misma al objeto DbDataAdapter
        /// </summary>
        /// <param name="adapter">DbDataAdapter a establecer transaccion</param>
        private void SetupTransactionAdapterCommands(DbDataAdapter adapter)
        {
            if (Transaction != null)
            {
                adapter.InsertCommand.Transaction = Transaction;
                adapter.UpdateCommand.Transaction = Transaction;
                adapter.DeleteCommand.Transaction = Transaction;
            }
        }

        #endregion Private Methods

        #region Constructors

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UniversalConnectorDAL()
        {
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="server">Tipo de servidor a utilizar</param>
        /// <param name="connectionString">Cadena de conexión</param>
        public UniversalConnectorDAL(ServerEnumType server, string connectionString)
        {
            this.Server = server;
            this.ConnectionString = connectionString;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Obtiene o establece el tipo de servidor de base de datos
        /// </summary>
        public ServerEnumType Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        /// <summary>
        /// Obtiene o establece la cadena de conexion
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Lee una secuencia sólo hacia delante de filas de un origen de datos.
        /// </summary>
        public DbDataReader DataReader
        {
            get
            {
                return _dataReader;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Abre una conexión de base de datos con los valores de propiedad que especifica ConnectionString
        /// </summary>
        public void Open()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// Cierra la conexión con la base de datos. Es el método preferido para cerrar cualquier conexión abierta.
        /// </summary>
        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// Inicia una transacción de base de datos.
        /// </summary>
        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                Open();
                Transaction = ConnectorFactory.GetTransaction(Connection);
            }

            Command.Transaction = Transaction;
        }

        /// <summary>
        /// Confirma la transacción de base de datos.
        /// </summary>
        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction = null;
            }
        }

        /// <summary>
        /// Deshace una transacción desde un estado pendiente.
        /// </summary>
        public void RollBack()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }
        }

        /// <summary>
        /// Limpia valores de los parametros.
        /// </summary>
        public void CleanParameters()
        {
            _parameters = null;
        }

        /// <summary>
        /// Establece los parametros que se van a utilizar.
        /// </summary>
        /// <param name="parameters">Parametros a utilizar</param>
        public void CreateParameters(object[] parameters)
        {
            _parameters = null;
            _parameters = parameters;
        }


        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        public void FillDataSet(ref DataSet dataSet, CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            Adapter.SelectCommand = Command;
            Adapter.Fill(dataSet);
            Command.Parameters.Clear();
        }

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="tableName">Nombre de la tabla en el clase DataSet</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        public void FillDataSet(ref DataSet dataSet, string tableName, CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            Adapter.SelectCommand = Command;
            Adapter.Fill(dataSet, tableName);
            Command.Parameters.Clear();
        }

        /// <summary>
        /// Agrega o actualiza filas en DataTable.
        /// </summary>
        /// <param name="table">Clase DataTable que se va a rellenar con registros.</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        public void FillDataTable(ref DataTable table, CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            Adapter.SelectCommand = Command;
            Adapter.Fill(table);
            Command.Parameters.Clear();
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados que devuelve la consulta. 
        /// Se omiten todas las demás columnas y filas.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Primera columna de la primera fila del conjunto de resultados.</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            object objectValue = Command.ExecuteScalar();
            Command.Parameters.Clear();
            return objectValue;
        }

        /// <summary>
        /// Ejecuta una instrucción SQL en un objeto de conexión.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Número de filas afectadas.</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            int objectValue = Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            return objectValue;
        }

        /// <summary>
        /// Ejecuta CommandText en Connection y devuelve un objeto DbDataReader.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DbDataReader.</returns>
        public void ExecuteReader(CommandType commandType, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            _dataReader = Command.ExecuteReader();
            Command.Parameters.Clear();
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DataSet</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            DataSet dataSet = new DataSet();
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            Adapter.SelectCommand = Command;
            Adapter.Fill(dataSet);
            Command.Parameters.Clear();
            return dataSet;
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DataTable</returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            DataTable dataTable = new DataTable();
            PrepareCommand(Command, Connection, Transaction, commandType, commandText, Parameters);
            Adapter.SelectCommand = Command;
            Adapter.Fill(dataTable);
            Command.Parameters.Clear();
            return dataTable;
        }

        /// <summary>
        /// Actualiza los valores de la base de datos ejecutando las instrucciones 
        /// INSERT, UPDATE o DELETE respectivas para cada fila insertada, 
        /// actualizada o eliminada en los objetos DataTable especificados.
        /// </summary>
        /// <param name="dataTable">Objeto DataTable que se utiliza para actualizar el origen de datos. </param>
        /// <returns>Número de filas del DataTable actualizadas correctamente.</returns>
        public int ExecuteBatch(DataTable dataTable, string commandText)
        {
            PrepareCommand(Command, Connection, Transaction, CommandType.Text, commandText, null);
            Adapter.SelectCommand = Command;
            FillAdapterCommands(Adapter);
            return Adapter.Update(dataTable);
        }

        #endregion Public Methods

        #region IDisposable Member

        // Indica si ya se llamo al método Dispose. (default = false)
        private bool disposed;

        /// <summary>
        /// Implementación de IDisposable. No se sobreescribe.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.Collect();
            // GC.SupressFinalize quita de la cola de finalización al objeto.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Limpia los recursos manejados y no manejados.
        /// </summary>
        /// <param name="disposing">
        /// Si es true, el método es llamado directamente o indirectamente
        /// desde el código del usuario.
        /// Si es false, el método es llamado por el finalizador
        /// y sólo los recursos no manejados son finalizados.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Preguntamos si Dispose ya fue llamado.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Llamamos al Dispose de todos los RECURSOS MANEJADOS.
                    Close();
                    this.Connection.Dispose();
                    CloseReader();
                    this.Command.Dispose();
                    this.Adapter.Dispose();
                    this._parameters = null;
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                    }
                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ...

            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~UniversalConnectorDAL()
        {
            this.Dispose(false);
        }

        #endregion IDisposable Member
    }
}
