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
    public class ContractTypeDTO
    {
        /// <summary>
        /// Identificador único de tipo de contrato
        /// </summary>
        [DataMember]
        public int ContractTypeId { get; set; }

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
        /// Descripción de tipo de contrato
        /// </summary>
        [DataMember]
        public string ContractTypeDescription { get; set; }

        /// <summary>
        /// Estado tipo de contrato: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string ContractTypeStatus { get; set; }
    }
}
