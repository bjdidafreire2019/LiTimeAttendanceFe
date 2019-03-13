using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Company
    /// </summary>
    [DataContract]
    public class CompanyDTO
    {
        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public int CompanyId { get; set; }

        /// <summary>
        /// Identificador único de usuario
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }

        /// <summary>
        /// Abreviatura de la empresa
        /// </summary>
        [DataMember]
        public string CompanyShortName { get; set; }

        /// <summary>
        /// Estado empresa: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string CompanyStatus { get; set; }
    }
}
