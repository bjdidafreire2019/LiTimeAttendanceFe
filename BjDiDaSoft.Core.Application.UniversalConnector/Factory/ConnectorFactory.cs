using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Factory
{
    sealed class ConnectorFactory
    {
        #region Constructors

        /// <summary>
        /// Constructor privado por defecto.
        /// </summary>
        private ConnectorFactory()
        {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Obtiene una conexion, según el servidor indicado
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <returns>Conexion implementada</returns>
        public static DbConnection GetConnection(ServerEnumType server)
        {
            DbConnection connection = null;
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    connection = new SqlConnection();
                    break;
                case ServerEnumType.MySQL:
                    connection = new MySqlConnection();
                    break;
                case ServerEnumType.Oracle:
                    connection = new OracleConnection();
                    break;
                default:
                    return null;
            }
            return connection;
        }

        /// <summary>
        /// Obtiene un objeto comando, según el servidor indicado
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <returns>Comando implementado</returns>
        public static DbCommand GetCommand(ServerEnumType server)
        {
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    return new SqlCommand();
                case ServerEnumType.MySQL:
                    return new MySqlCommand();
                case ServerEnumType.Oracle:
                    return new OracleCommand();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Obtiene un adaptador, según el servidor indicado
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <returns></returns>
        public static DbDataAdapter GetDataAdapter(ServerEnumType server)
        {
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    return new SqlDataAdapter();
                case ServerEnumType.MySQL:
                    return new MySqlDataAdapter();
                case ServerEnumType.Oracle:
                    return new OracleDataAdapter();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Obtiene un objecto Transaccion, según la conexion generica indicada
        /// </summary>
        /// <param name="connection">Conexion a utilizar</param>
        /// <returns>Transaccion implementada</returns>
        public static DbTransaction GetTransaction(DbConnection connection)
        {
            DbTransaction transaction = connection.BeginTransaction();
            return transaction;
        }

        /// <summary>
        /// Obtiene un parametro, según el servidor indicado
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <returns>Parametro implementado</returns>
        public static DbParameter GetParameter(ServerEnumType server)
        {
            DbParameter parameter = null;
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    parameter = new SqlParameter();
                    break;
                case ServerEnumType.MySQL:
                    parameter = new MySqlParameter();
                    break;
                case ServerEnumType.Oracle:
                    parameter = new OracleParameter();
                    break;
                default:
                    return null;
            }
            return parameter;
        }

        /// <summary>
        /// Recupera información de parámetro del procedimiento almacenado especificado en SqlCommand 
        /// y rellena la colección de Parameters del objeto SqlCommand especificado.
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <param name="command">Commando a rellenar con parametros</param>
        public static void DeriveParameter(ServerEnumType server, DbCommand command)
        {
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    SqlCommandBuilder.DeriveParameters(command as SqlCommand);
                    command.Parameters.RemoveAt("@RETURN_VALUE");
                    break;
                case ServerEnumType.MySQL:
                    MySqlCommandBuilder.DeriveParameters(command as MySqlCommand);
                    break;
                case ServerEnumType.Oracle:
                    OracleCommandBuilder.DeriveParameters(command as OracleCommand);
                    break;
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase DbCommandBuilder con el objeto SqlDataAdapter asociado.
        /// </summary>
        /// <param name="server">Servidor a utilizar</param>
        /// <param name="adapter">Adapater a quien se le rellenan los comandos</param>
        public static DbCommandBuilder GetCommandBuilder(ServerEnumType server, DbDataAdapter adapter)
        {
            switch (server)
            {
                case ServerEnumType.SqlServer:
                    return new SqlCommandBuilder(adapter as SqlDataAdapter);
                case ServerEnumType.MySQL:
                    return new MySqlCommandBuilder(adapter as MySqlDataAdapter);
                case ServerEnumType.Oracle:
                    return new OracleCommandBuilder(adapter as OracleDataAdapter);
                default:
                    return null;
            }
        }

        #endregion Methods
    }
}
