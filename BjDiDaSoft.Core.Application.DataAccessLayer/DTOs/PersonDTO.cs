using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Person
    /// </summary>
    [DataContract]
    public class PersonDTO
    {
        /// <summary>
        /// Identificador único de persona
        /// </summary>
        [DataMember]
        public int PersonId { get; set; }

        /// <summary>
        /// Número identificación de persona
        /// </summary>
        [DataMember]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Apellidos y nombres completos de persona
        /// </summary>
        [DataMember]
        public string PersonName { get; set; }

        /// <summary>
        /// Fecha nacimiento de persona
        /// </summary>
        [DataMember]
        public DateTime PersonBirthDate { get; set; }

        /// <summary>
        /// Peso en kg de persona
        /// </summary>
        [DataMember]
        public Decimal PersonWeight { get; set; }

        /// <summary>
        /// Estado activo o desactivo de persona
        /// </summary>
        [DataMember]
        public string PersonStatus { get; set; }
    }
}
