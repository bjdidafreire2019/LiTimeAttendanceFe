using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Workday
    /// </summary>
    [DataContract]
    public class WorkdayDTO
    {
        /// <summary>
        /// Identificador único de jornada
        /// </summary>
        [DataMember]
        public int WorkdayId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Abreviatura de jornada laboral
        /// </summary>
        [DataMember]
        public string WorkdayShortName { get; set; }

        /// <summary>
        /// Descripción de jornada
        /// </summary>
        [DataMember]
        public string WorkdayDescription { get; set; }

        /// <summary>
        /// Estado de jornada: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string WorkdayStatus { get; set; }
    }
}
