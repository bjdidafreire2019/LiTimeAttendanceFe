using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de ScheduleEmployeeReader
    /// </summary>
    [DataContract]
    public class ScheduleEmployeeReaderDTO
    {
        /// <summary>
        /// Identificador único de horario
        /// </summary>
        [DataMember]
        public ScheduleDTO Schedule { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Lector
        /// </summary>
        [DataMember]
        public ReaderDTO Reader { get; set; }

        /// <summary>
        /// Identificador único de empleado
        /// </summary>
        [DataMember]
        public EmployeeDTO Employee { get; set; }

        /// <summary>
        /// Identificador único de zona de un lector
        /// </summary>
        [DataMember]
        public int ZoneNumber { get; set; }

        /// <summary>
        /// Hora inicio ingreso
        /// </summary>
        [DataMember]
        public string ScheduleStartTimeIncome { get; set; }

        /// <summary>
        /// Hora fin ingreso
        /// </summary>
        [DataMember]
        public string ScheduleEndTimeIncome { get; set; }

        /// <summary>
        /// Hora inicio desayuno, almuerzo, merienda, refrigerio, cena 
        /// </summary>
        [DataMember]
        public string ScheduleStartTimeLunch { get; set; }

        /// <summary>
        /// Hora fin desayuno, almuerzo, merienda, refrigerio, cena 
        /// </summary>
        [DataMember]
        public string ScheduleEndTimeLunch { get; set; }

        /// <summary>
        /// Hora inicio salida
        /// </summary>
        [DataMember]
        public string ScheduleStartTimeOutput { get; set; }

        /// <summary>
        /// Hora fin salida
        /// </summary>
        [DataMember]
        public string ScheduleEndTimeOutput { get; set; }

        /// <summary>
        /// Hora inicio
        /// </summary>
        [DataMember]
        public string ScheduleStartTime { get; set; }

        /// <summary>
        /// Hora fin
        /// </summary>
        [DataMember]
        public string ScheduleEndTime { get; set; }

        /// <summary>
        /// Hora desayuno, almuerzo, merienda, refrigerio, cena 
        /// </summary>
        [DataMember]
        public string ScheduleLunchTime { get; set; }

        /// <summary>
        /// Trama zona de tiempo del lector
        /// </summary>
        [DataMember]
        public string ScheduleDataFrame { get; set; }

        /// <summary>
        /// Acceso libre o restringido
        /// </summary>
        [DataMember]
        public string ScheduleAccess { get; set; }

    }
}
