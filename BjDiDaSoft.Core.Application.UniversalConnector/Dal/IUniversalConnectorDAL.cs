using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Dal
{
    interface IUniversalConnectorDAL : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// Obtiene o establece el tipo de servidor de base de datos
        /// </summary>
        ServerEnumType Server { get; set; }

        /// <summary>
        /// Obtiene o establece la cadena de conexion
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Lee una secuencia sólo hacia delante de filas de un origen de datos.
        /// </summary>
        DbDataReader DataReader { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Abre una conexión de base de datos con los valores de propiedad que especifica ConnectionString
        /// </summary>
        void Open();

        /// <summary>
        /// Cierra la conexión con la base de datos. Es el método preferido para cerrar cualquier conexión abierta.
        /// </summary>
        void Close();

        /// <summary>
        /// Inicia una transacción de base de datos.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Confirma la transacción de base de datos.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Deshace una transacción desde un estado pendiente.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Limpia valores de los parametros.
        /// </summary>
        void CleanParameters();

        /// <summary>
        /// Establece los parametros que se van a utilizar.
        /// </summary>
        /// <param name="parameters">Parametros a utilizar</param>
        void CreateParameters(object[] parameters);

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        void FillDataSet(ref DataSet dataSet, CommandType commandType, string commandText);

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="tableName">Nombre de la tabla en el clase DataSet</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        void FillDataSet(ref DataSet dataSet, string tableName, CommandType commandType, string commandText);

        /// <summary>
        /// Agrega o actualiza filas en DataTable.
        /// </summary>
        /// <param name="table">Clase DataTable que se va a rellenar con registros.</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        void FillDataTable(ref DataTable table, CommandType commandType, string commandText);

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados que devuelve la consulta. 
        /// Se omiten todas las demás columnas y filas.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Primera columna de la primera fila del conjunto de resultados.</returns>
        object ExecuteScalar(CommandType commandType, string commandText);

        /// <summary>
        /// Ejecuta una instrucción SQL en un objeto de conexión.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Número de filas afectadas.</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);

        /// <summary>
        /// Ejecuta CommandText en Connection y devuelve un objeto DbDataReader.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DbDataReader.</returns>
        void ExecuteReader(CommandType commandType, string commandText);

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DataSet</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText);

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <returns>Un objeto DataTable</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText);

        /// <summary>
        /// Actualiza los valores de la base de datos ejecutando las instrucciones 
        /// INSERT, UPDATE o DELETE respectivas para cada fila insertada, 
        /// actualizada o eliminada en los objetos DataTable especificados.
        /// </summary>
        /// <param name="dataTable">Objeto DataTable que se utiliza para actualizar el origen de datos. </param>
        /// <returns>Número de filas del DataTable actualizadas correctamente.</returns>
        int ExecuteBatch(DataTable dataTable, string commandText);



        #endregion Public Methods
    }
}
