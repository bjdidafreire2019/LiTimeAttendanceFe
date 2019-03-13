using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Role
    /// </summary>
    [DataContract]
    public class RoleDTO
    {
        /// <summary>
        /// Identificador único de rol
        /// </summary>
        [DataMember]
        public int RoleId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Descripción del rol
        /// </summary>
        [DataMember]
        public string RoleDescription { get; set; }

        /// <summary>
        /// Estado del rol: A (Activado), D (Desactivado)
        /// </summary>
        [DataMember]
        public string RoleStatus { get; set; }
    }
}
