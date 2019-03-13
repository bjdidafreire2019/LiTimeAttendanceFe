using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de centro de gasto
    /// </summary>
    [DataContract]
    public class SpendingCenterDTO
    {
        /// <summary>
        /// Identificador único de centro de gasto
        /// </summary>
        [DataMember]
        public int SpendingCenterId { get; set; }

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
        /// Descripción del centro de gasto
        /// </summary>
        [DataMember]
        public string SpendingCenterDescription { get; set; }

        /// <summary>
        /// Estado del centro de gasto: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string SpendingCenterStatus { get; set; }
    }
}
