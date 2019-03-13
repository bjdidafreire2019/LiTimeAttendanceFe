using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Reader
    /// </summary>
    [DataContract]
    public class ReaderDTO
    {
        /// <summary>
        /// Identificador único de lector
        /// </summary>
        [DataMember]
        public int ReaderId { get; set; }

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
        /// Número serial del lector
        /// </summary>
        [DataMember]
        public string ReaderSerialNumber { get; set; }

        /// <summary>
        /// Nombre y/o descripción del lector
        /// </summary>
        [DataMember]
        public string ReaderName { get; set; }

        /// <summary>
        /// Tipo de lector: 
        ///  ED --> Entrada departamento
        ///  SD --> Salida departamento
        ///  EA --> Entrada asistencia
        ///  SA --> Salida asistencia
        /// </summary>
        [DataMember]
        public string ReaderType { get; set; }

        /// <summary>
        /// Estado comunicación del lector
        /// </summary>
        [DataMember]
        public string ReaderStatusComm { get; set; }

        /// <summary>
        /// Estado lector: A (Activo), D (Desactivado)
        /// </summary>
        [DataMember]
        public string ReaderStatus { get; set; }
    }
}
