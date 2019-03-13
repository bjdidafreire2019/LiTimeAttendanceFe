using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Logic
{
    interface IUniversalConnectorLogic : IDisposable
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
        /// Inicia una transacción de base de datos.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Confirma la transacción de base de datos.
        /// </summary>
        void Commit();

        /// <summary>
        /// Deshace una transacción desde un estado pendiente.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataSet.</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Ejecuta la consulta y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Un objeto DataTable.</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados devuelto por la consulta. 
        /// Las demás columnas o filas no se tienen en cuenta.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Primera columna de la primera fila del conjunto de resultados.</returns>
        object ExecuteScalar(CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Ejecuta una instrucción SQL en un objeto de conexión.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        /// <returns>Número de filas afectadas.</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Ejecuta CommandText en Connection y devuelve un objeto DbDataReader.
        /// </summary>
        /// <param name="commandType">Especifica cómo se interpreta una cadena de comando.</param>
        /// <param name="commandText">Establece el comando de texto que se debe ejecutar en el origen de datos.</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        void ExecuteReader(CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Actualiza los valores de la base de datos ejecutando las instrucciones 
        /// INSERT, UPDATE o DELETE respectivas para cada fila insertada, 
        /// actualizada o eliminada en los objetos DataTable especificados.
        /// </summary>
        /// <param name="dataTable">Objeto DataTable que se utiliza para actualizar el origen de datos. </param>
        /// <returns>Número de filas del DataTable actualizadas correctamente.</returns>
        int ExecuteBatch(DataTable dataTable);

        /// <summary>
        /// Agrega filas en un intervalo especificado de DataTable.
        /// NOTA: La consulta SQL se genera automaticamente.
        /// Si estable parametros deben conincidir con llaves primarias del objeto DataTable
        /// </summary>
        /// <param name="dataTable">Nombre de DataTable que se va a utilizar para la asignación de tabla. </param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        void AutomaticFill(DataTable dataTable, params object[] parameters);

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        void FillDataSet(DataSet dataSet, CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Agrega o actualiza filas en DataSet.
        /// </summary>
        /// <param name="dataSet">Clase DataSet que se va a rellenar con registros y, si es necesario, con un esquema. </param>
        /// <param name="tableName">Nombre de la tabla en el clase DataSet</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        void FillDataSet(DataSet dataSet, string tableName, CommandType commandType, string commandText, params object[] parameters);

        /// <summary>
        /// Agrega o actualiza filas en DataTable.
        /// </summary>
        /// <param name="table">Clase DataTable que se va a rellenar con registros.</param>
        /// <param name="commandType">Tipo de comando</param>
        /// <param name="commandText">Consulta SQL a ejecutar</param>
        /// <param name="parameters">Establece los parametros que se va usar en la consulta.</param>
        void FillDataTable(DataTable table, CommandType commandType, string commandText, params object[] parameters);

        #endregion Public Methods
    }
}
