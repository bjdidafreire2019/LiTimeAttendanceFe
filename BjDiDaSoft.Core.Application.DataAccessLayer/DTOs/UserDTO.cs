using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de User
    /// </summary>
    [DataContract]
    public class UserDTO
    {
        /// <summary>
        /// Identificador único de usuario
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Empleado
        /// </summary>
        [DataMember]
        public EmployeeDTO Employee { get; set; }

        /// <summary>
        /// Rol
        /// </summary>
        [DataMember]
        public RoleDTO Role { get; set; }

        /// <summary>
        /// Login usuario
        /// </summary>
        [DataMember]
        public string UserLogin { get; set; }

        /// <summary>
        /// Contraseña usuario
        /// </summary>
        [DataMember]
        public string UserPassword { get; set; }

        /// <summary>
        /// Es supervisor el usuario
        /// </summary>
        [DataMember]
        public string UserSupervisor { get; set; }

        /// <summary>
        /// Estado activo / inactivo usuario
        /// </summary>
        [DataMember]
        public string UserStatus { get; set; }
    }
}
