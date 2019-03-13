using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.UniversalConnector.Servers
{
    /// <summary>
    /// Servidores para establecer conexiones.
    /// </summary>
    public enum ServerEnumType
    {
        /// <summary>
        /// No indica servidor de base de datos.
        /// </summary>
        None,

        /// <summary>
        /// Base de datos Microsoft SQL Server.
        /// </summary>
        SqlServer,

        /// <summary>
        /// Base de datos MySQL.
        /// </summary>
        MySQL,

        /// <summary>
        /// Base de datos Oracle.
        /// </summary>
        Oracle,

        /// <summary>
        /// Fuentes de datos Odbc
        /// </summary>
        Odbc,

        /// <summary>
        /// Fuentes de datos OleDb
        /// </summary>
        OleDb
    }
}
