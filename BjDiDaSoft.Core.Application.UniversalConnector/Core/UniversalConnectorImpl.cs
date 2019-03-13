using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.UniversalConnector.Servers;
using BjDiDaSoft.Core.Application.UniversalConnector.Logic;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Core
{
    public class UniversalConnectorImpl : IUniversalConnector
    {
        #region Private Member Variables

        private IUniversalConnectorLogic _universalConnectorLogic;

        #endregion Private Member Variables

        #region Private Properties

        /// <summary>
        /// Obtiene o establece el objeto IDataConnectorLogic
        /// </summary>
        private IUniversalConnectorLogic UniversalConnectorLogic
        {
            get
            {
                if (_universalConnectorLogic == null)
                {
                    _universalConnectorLogic = new UniversalConnectorLogic();
                    _universalConnectorLogic.Server = Server;
                    _universalConnectorLogic.ConnectionString = ConnectionString;
                }
                return _universalConnectorLogic;
            }
            set
            {
                _universalConnectorLogic = value;
            }
        }

        #endregion Private Properties

        #region Constructors

        /// <summary>
        /// Constructor por defecto, el servidor de base de datos predeterminado es SQL Server
        /// </summary>
        /// <param name="connectionString">Cadena de conexión</param>
        public UniversalConnectorImpl(string connectionString)
        {
            this.Server = ServerEnumType.SqlServer;
            this.ConnectionString = connectionString;
            this.CleanBeforeFill = true;
        }


        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="server">Tipo de servidor a utilizar</param>
        /// <param name="connectionString">Cadena de conexión</param>
        public UniversalConnectorImpl(ServerEnumType server, string connectionString)
        {
            this.Server = server;
            this.ConnectionString = connectionString;
            this.CleanBeforeFill = true;
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="server">Tipo de servidor a utilizar</param>
        /// <param name="connectionString">Cadena de conexión</param>
        /// <param name="cleanBeforeFill">Indica si los DataSet o DataTable se limpian antes de llenarlos</param>
        public UniversalConnectorImpl(ServerEnumType server, string connectionString, bool cleanBeforeFill)
        {
            this.Server = server;
            this.ConnectionString = connectionString;
            this.CleanBeforeFill = cleanBeforeFill;
        }

        #endregion Constructors

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
        public virtual DbDataReader DataReader
        {
            get
            {
                return UniversalConnectorLogic.DataReader;
            }
        }

        /// <summary>
        /// Indica si debe limpiar los DataSet o DataTable, antes de llenarlos
        /// </summary>
        public virtual bool CleanBeforeFill
        {
            get;
            set;
        }

        /// <summary>
        /// Indica si el objeto hizo llamado al metodo Dispose
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inicia una transacción de base de datos.
        /// </summary>
        public virtual void BeginTransaction()
        {
            UniversalConnectorLogic.BeginTransaction();
        }

        /// <summary>
        /// Confirma la transacción de base de datos.
        /// </summary>
        public virtual void Commit()
        {
            UniversalConnectorLogic.Commit();
        }

        /// <summary>
        /// Deshace una transacción desde un estado pendiente.
        /// </summary>
        public virtual void RollBack()
        {
            UniversalConnectorLogic.RollBack();
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataSet.</returns>
        public virtual DataSet ExecuteDataSet(CommandType commandType, string commandText, params object[] parameters)
        {
            return UniversalConnectorLogic.ExecuteDataSet(commandType, commandText, parameters);
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataTable.</returns>
        public virtual DataTable ExecuteDataTable(CommandType commandType, string commandText, params object[] parameters)
        {
            return UniversalConnectorLogic.ExecuteDataTable(commandType, commandText, parameters);
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados devuelto por la consulta. 
        /// Las demás columnas o filas no se tienen en cuenta.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Primera columna de la primera fila del conjunto de resultados.</returns>
        public virtual object ExecuteScalar(CommandType commandType, string commandText, params object[] parameters)
        {
            return UniversalConnectorLogic.ExecuteScalar(commandType, commandText, parameters);
        }

        /// <summary>
        /// Ejecuta una instrucción SQL en un objeto de conexión.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Número de filas afectadas.</returns>
        public virtual int ExecuteNonQuery(CommandType commandType, string commandText, params object[] parameters)
        {
            return UniversalConnectorLogic.ExecuteNonQuery(commandType, commandText, parameters);
        }

        /// <summary>
        /// Ejecuta CommandText en Connection y devuelve un objeto DbDataReader.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public virtual void ExecuteReader(CommandType commandType, string commandText, params object[] parameters)
        {
            UniversalConnectorLogic.ExecuteReader(commandType, commandText, parameters);
        }

        /// <summary>
        /// Actualiza los valores de la base de datos ejecutando las instrucciones 
        /// INSERT, UPDATE o DELETE respectivas para cada fila insertada, 
        /// actualizada o eliminada en los objetos DataTable especificados.
        /// </summary>
        /// <param name="dataTable">Objeto DataTable que se utiliza para actualizar el origen de datos. </param>
        /// <returns>Número de filas del DataTable actualizadas correctamente.</returns>
        public virtual int ExecuteBatch(DataTable dataTable)
        {
            return UniversalConnectorLogic.ExecuteBatch(dataTable);
        }

        /// <summary>
        /// Agrega filas en un intervalo especificado de DataTable.
        /// NOTA: La consulta SQL se genera automaticamente.
        /// Si estable parametros deben conincidir con llaves primarias del objeto DataTable
        /// </summary>
        /// <param name="dataTable">Nombre de DataTable que se va a utilizar para la asignación de tabla. </param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public virtual void AutomaticFill(DataTable dataTable, params object[] parameters)
        {
            if (CleanBeforeFill)
            {
                dataTable.Clear();
            }
            UniversalConnectorLogic.AutomaticFill(dataTable, parameters);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public virtual void FillDataSet(DataSet dataSet, CommandType commandType, string commandText, params object[] parameters)
        {
            if (CleanBeforeFill)
            {
                dataSet.Clear();
            }
            UniversalConnectorLogic.FillDataSet(dataSet, commandType, commandText, parameters);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="tableName">Nombre de la tabla en el clase DataSet</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public virtual void FillDataSet(DataSet dataSet, string tableName, CommandType commandType, string commandText, params object[] parameters)
        {
            if (CleanBeforeFill)
            {
                if (dataSet.Tables.Contains(tableName))
                    dataSet.Tables[tableName].Clear();
            }

            UniversalConnectorLogic.FillDataSet(dataSet, tableName, commandType, commandText, parameters);
        }

        /// <summary>
        /// Agrega o actualiza filas en DataTable.
        /// </summary>
        /// <param name="table">Clase DataTable que se va a rellenar con registros.</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        public virtual void FillDataTable(DataTable table, CommandType commandType, string commandText, params object[] parameters)
        {
            if (CleanBeforeFill)
            {
                table.Clear();
            }

            UniversalConnectorLogic.FillDataTable(table, commandType, commandText, parameters);
        }

        #endregion

        #region IDisposable Member

        /// <summary>
        /// Indica si ya se llamo al método Dispose. (default = false)
        /// </summary>
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
                    UniversalConnectorLogic.Dispose();
                    this.ConnectionString = string.Empty;
                    this.Server = ServerEnumType.None;
                    if (this.DataReader != null)
                    {
                        DataReader.Dispose();
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
        ~UniversalConnectorImpl()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
