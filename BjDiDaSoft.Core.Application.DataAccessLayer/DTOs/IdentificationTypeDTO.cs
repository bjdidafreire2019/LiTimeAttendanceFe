using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de departamento
    /// </summary>
    [DataContract]
    public class IdentificationTypeDTO
    {
        /// <summary>
        /// Identificador único tipo de identificación
        /// </summary>
        [DataMember]
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Descripción tipo de identificación
        /// </summary>
        [DataMember]
        public string IdentificationTypeDescription { get; set; }

        /// <summary>
        /// Estado del tipo de identificación: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string IdentificationTypeStatus { get; set; }
    }
}
