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
    public class SexDTO
    {
        /// <summary>
        /// Identificador único de sexo
        /// </summary>
        [DataMember]
        public int SexId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Descripción de sexo
        /// </summary>
        [DataMember]
        public string SexDescription { get; set; }

        /// <summary>
        /// Abreviatura de sexo
        /// </summary>
        [DataMember]
        public string SexShortName { get; set; }

        /// <summary>
        /// Estado del sexo: A (Activado), D (Desactivado)
        /// </summary>
        [DataMember]
        public string RoleStatus { get; set; }
    }
}
