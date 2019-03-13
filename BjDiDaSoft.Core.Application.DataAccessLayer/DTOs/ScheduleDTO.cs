using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Schedule
    /// </summary>
    [DataContract]
    public class ScheduleDTO
    {
        /// <summary>
        /// Identificador único de horario
        /// </summary>
        [DataMember]
        public int ScheduleId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Identificador único de jornada
        /// </summary>
        [DataMember]
        public WorkdayDTO Workday { get; set; }

        /// <summary>
        /// Identificador único de usuario
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Abreviatura de horario
        /// </summary>
        [DataMember]
        public string ScheduleShortName { get; set; }

        /// <summary>
        /// Descripción de horario
        /// </summary>
        [DataMember]
        public string ScheduleDescription { get; set; }

        /// <summary>
        /// Hora inicio de horario
        /// </summary>
        [DataMember]
        public String ScheduleStartHour { get; set; }

        /// <summary>
        /// Hora inicio de horario formateada a hh:mm
        /// </summary>
        [DataMember]
        public String StartHour { get; set; }

        /// <summary>
        /// Hora fin de horario
        /// </summary>
        [DataMember]
        public String ScheduleEndHour { get; set; }

        /// <summary>
        /// Hora fin de horario formateada a hh:mm
        /// </summary>
        [DataMember]
        public String EndHour { get; set; }

        /// <summary>
        /// Hora desayuno, almuerzo, merienda, refigerio, cena
        /// </summary>
        [DataMember]
        public String ScheduleLunchHour { get; set; }

        /// <summary>
        /// Hora desayuno, almuerzo, merienda, refigerio, cena formateada a hh:mm
        /// </summary>
        [DataMember]
        public String LunchHour { get; set; }

        /// <summary>
        /// Esta en horario nocturno
        /// </summary>
        [DataMember]
        public String ScheduleIsNight { get; set; }

        /// <summary>
        /// Tiene acceso libre o restringuido
        /// </summary>
        [DataMember]
        public String ScheduleAccess { get; set; }

        /// <summary>
        /// Número de zona para salida
        /// </summary>
        [DataMember]
        public int ScheduleOuterZone { get; set; }

        /// <summary>
        /// Número de zona para entrada
        /// </summary>
        [DataMember]
        public int ScheduleInnerZone { get; set; }

        /// <summary>
        /// Minutos que se demora el desayuno, almuerzo, merienda, refrigerio o cena
        /// </summary>
        [DataMember]
        public int ScheduleLunchTime { get; set; }

        /// <summary>
        /// Timepo que se espera para iniciar la salida
        /// </summary>
        [DataMember]
        public int ScheduleOutputDelay { get; set; }

        /// <summary>
        /// Tiempo que se epera para terminar la entrada
        /// </summary>
        [DataMember]
        public int ScheduleEntryDelay { get; set; }

        /// <summary>
        /// Estado del horario: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string ScheduleStatus { get; set; }
    }
}
