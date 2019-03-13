using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Charge
    /// </summary>
    [DataContract]
    public class ChargeDTO
    {
        /// <summary>
        /// Identificador único de cargo en la empresa
        /// </summary>
        [DataMember]
        public int ChargeId { get; set; }

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
        /// Descripción del cargo
        /// </summary>
        [DataMember]
        public string ChargeDescription { get; set; }

        /// <summary>
        /// Estado del cargo: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string ChargeStatus { get; set; }
    }
}
