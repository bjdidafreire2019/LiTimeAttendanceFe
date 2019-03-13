using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Factory
{
    /// <summary>
    /// Se encarga de generar consultas automaticas a partir de objetos DataTable.
    /// </summary>
    class SqlStatementsFactory
    {
        #region Constants

        private const string tableNullError = "The DataTable object can not be null.";
        private const string tableNameNullError = "The table name can not be empty.";
        private const string tableColumnError = "The DataTable object must contain at least one column.";
        private const string primaryKeyError = "The DataTable object must contain at least one primary key, to generate automatic query.";
        private const string tableParam = "dataTable";
        private const string tableNameParam = "dataTable.TableName";
        private const string primaryKeyParam = "dataTable.PrimaryKey";

        #endregion Constants

        #region Private Methods

        /// <summary>
        /// Valida que el objeto DataTable no contenga parametros invalidos
        /// </summary>
        /// <param name="dataTable">DataTable a evaluar</param>
        private static void InitialValidation(DataTable dataTable)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException(tableParam, tableNullError);
            }

            if (string.IsNullOrEmpty(dataTable.TableName))
            {
                throw new ArgumentNullException(tableNameParam, tableNameNullError);
            }

            if (dataTable.Columns.Count == 0)
            {
                throw new ArgumentException(tableColumnError);
            }
        }

        /// <summary>
        /// Valida que el objeto DataTable contenga llaves primarias para generar la clausula
        /// WHERE automaticamente.
        /// </summary>
        /// <param name="dataTable">DataTable a evaluar</param>
        private static void WhereClauseValidation(DataTable dataTable)
        {
            if (dataTable.PrimaryKey.Count() == 0)
            {
                throw new ArgumentException(primaryKeyError, primaryKeyParam);
            }
        }

        /// <summary>
        /// Crear la cluasula WHERE
        /// </summary>
        /// <param name="stringBuilder">Es donde se almacenará la sentencias construida</param>
        /// <param name="dataTable">Tabla a contruir</param>
        private static void CreateWhereClause(ref StringBuilder stringBuilder, DataTable dataTable)
        {
            WhereClauseValidation(dataTable);

            bool isFirstColumn = true;
            foreach (DataColumn c in dataTable.PrimaryKey)
            {
                if (isFirstColumn)
                {
                    isFirstColumn = false;
                }
                else
                {
                    stringBuilder.Append(" AND ");
                }
                stringBuilder.Append(c.ColumnName);
                stringBuilder.Append(" = @");
                stringBuilder.Append(c.ColumnName);
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Genera una consulta SELECT apartir de un System.Data.DataTable.
        /// </summary>
        /// <param name="dataTable">DataTable para generar la consulta.</param>
        /// <param name="useWhere">Indica si debe generar la clausula WHERE.</param>
        /// <returns>Consulta SELECT</returns>
        public static string SelectCommand(DataTable dataTable, bool useWhere = false)
        {
            InitialValidation(dataTable);

            StringBuilder sql = new StringBuilder("SELECT ");
            StringBuilder values = new StringBuilder(" WHERE ");
            bool isFirstColumn = true;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (isFirstColumn)
                {
                    isFirstColumn = false;
                }
                else
                {
                    sql.Append(", ");
                }

                sql.Append(column.ColumnName);
            }

            sql.Append(" FROM " + dataTable.TableName);

            if (useWhere)
            {
                CreateWhereClause(ref values, dataTable);

                sql.Append(values.ToString());

            }
            sql.Append(";");
            return sql.ToString();
        }

        #endregion Public Methods
    }
}
