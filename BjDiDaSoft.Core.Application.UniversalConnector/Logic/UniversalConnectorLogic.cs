using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.UniversalConnector.Dal;
using BjDiDaSoft.Core.Application.UniversalConnector.Factory;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;


namespace BjDiDaSoft.Core.Application.UniversalConnector.Logic
{
    class UniversalConnectorLogic : IUniversalConnectorLogic
    {
        #region Private Member Variables

        private IUniversalConnectorDAL _universalConnectorDal;

        #endregion Private Member Variables

        #region Private Properties

        /// <summary>
        /// Obtiene o establece el objeto IDataConnectorDAL
        /// </summary>
        private IUniversalConnectorDAL UniversalConnectorDal
        {
            get
            {
                if (_universalConnectorDal == null)
                {
                    _universalConnectorDal = new UniversalConnectorDAL(Server, ConnectionString);
                }
                return _universalConnectorDal;
            }
            set
            {
                _universalConnectorDal = value;
            }
        }

        #endregion Private Properties

        #region Public Properties

        /// <summary>
        /// Obtiene o establece el tipo de servidor de base de datos
        /// </summary>
        public ServerEnumType Server
        {
            get;
            set;
        }

        /// <summary>
        /// Obtiene o establece la cadena de conexion
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Lee una secuencia sólo hacia delante de filas de un origen de datos.
        /// </summary>
        public DbDataReader DataReader
        {
            get
            {
                return UniversalConnectorDal.DataReader;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Inicia una transacción de base de datos.
        /// </summary>
        public void BeginTransaction()
        {
            UniversalConnectorDal.BeginTransaction();
        }

        /// <summary>
        /// Confirma la transacción de base de datos.
        /// </summary>
        public void Commit()
        {
            UniversalConnectorDal.CommitTransaction();
        }

        /// <summary>
        /// Deshace una transacción desde un estado pendiente.
        /// </summary>
        public void RollBack()
        {
            UniversalConnectorDal.RollBack();
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataSet.</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            return UniversalConnectorDal.ExecuteDataSet(commandType, commandText);
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataTable.</returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            return UniversalConnectorDal.ExecuteDataTable(commandType, commandText);
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados devuelto por la consulta. 
        /// Las demás columnas o filas no se tienen en cuenta.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Primera columna de la primera fila del conjunto de resultados.</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            return UniversalConnectorDal.ExecuteScalar(commandType, commandText);
        }

        /// <summary>
        /// Ejecuta una instrucción SQL en un objeto de conexión.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            return UniversalConnectorDal.ExecuteNonQuery(commandType, commandText);

        }

        /// <summary>
        /// Ejecuta CommandText en Connection y devuelve un objeto DbDataReader.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public void ExecuteReader(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            UniversalConnectorDal.ExecuteReader(commandType, commandText);
        }

        /// <summary>
        /// Actualiza los valores de la base de datos ejecutando las instrucciones 
        /// INSERT, UPDATE o DELETE respectivas para cada fila insertada, 
        /// actualizada o eliminada en los objetos DataTable especificados.
        /// </summary>
        /// <param name="dataTable">Objeto DataTable que se utiliza para actualizar el origen de datos. </param>
        /// <returns>Número de filas del DataTable actualizadas correctamente.</returns>
        public int ExecuteBatch(DataTable dataTable)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.Open();
            return UniversalConnectorDal.ExecuteBatch(dataTable, SqlStatementsFactory.SelectCommand(dataTable));
        }

        /// <summary>
        /// Agrega filas en un intervalo especificado de DataTable.
        /// NOTA: La consulta SQL se genera automaticamente.
        /// Si estable parametros deben conincidir con llaves primarias del objeto DataTable
        /// </summary>
        /// <param name="dataTable">Nombre de DataTable que se va a utilizar para la asignación de tabla. </param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public void AutomaticFill(DataTable dataTable, params object[] parameters)
        {
            dataTable.Clear();
            UniversalConnectorDal.CleanParameters();

            string selectCmd = string.Empty;

            if (parameters.Length == 0)
            {
                selectCmd = SqlStatementsFactory.SelectCommand(dataTable);
            }

            else
            {
                selectCmd = SqlStatementsFactory.SelectCommand(dataTable, true);
                UniversalConnectorDal.CreateParameters(parameters);
            }

            UniversalConnectorDal.Open();
            UniversalConnectorDal.FillDataTable(ref dataTable, CommandType.Text, selectCmd);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public void FillDataSet(DataSet dataSet, CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            UniversalConnectorDal.FillDataSet(ref dataSet, commandType, commandText);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="tableName">Nombre de la tabla en el clase DataSet</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public void FillDataSet(DataSet dataSet, string tableName, CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            UniversalConnectorDal.FillDataSet(ref dataSet, tableName, commandType, commandText);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataTable.
        /// </summary>
        /// <param name="table">Clase DataTable que se va a rellenar con registros.</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public void FillDataTable(DataTable table, CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorDal.CleanParameters();
            UniversalConnectorDal.CreateParameters(parameters);

            UniversalConnectorDal.Open();
            UniversalConnectorDal.FillDataTable(ref table, commandType, commandText);
        }

        #endregion Methods

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
                    UniversalConnectorDal.Dispose();
                }

                // Acá finalizamos correctamente los RECURSOS NO MANEJADOS
                // ...

            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~UniversalConnectorLogic()
        {
            this.Dispose(false);
        }

        #endregion IDisposable Member
    }
}
