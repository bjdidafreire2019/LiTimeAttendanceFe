using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de ContractType
    /// </summary>
    [DataContract]
    public class PermissionTypeDTO
    {
        /// <summary>
        /// Identificador único de tipo de permiso
        /// </summary>
        [DataMember]
        public int PermissionTypeId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Identificador único de usuario
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Descripción de tipo de permiso
        /// </summary>
        [DataMember]
        public string PermissionTypeDescription { get; set; }

        /// <summary>
        /// Permiso laboral o personal
        /// </summary>
        [DataMember]
        public String PermissionTypeType { get; set; }

        /// <summary>
        /// Estado tipo de permiso: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string PermissionTypeStatus { get; set; }
    }
}
