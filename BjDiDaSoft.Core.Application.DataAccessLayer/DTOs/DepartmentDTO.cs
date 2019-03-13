using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de Department
    /// </summary>
    [DataContract]
    public class DepartmentDTO
    {
        /// <summary>
        /// Identificador único de departamento
        /// </summary>
        [DataMember]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public CompanyDTO Company { get; set; }

        /// <summary>
        /// Identificador único de centro de gasto
        /// </summary>
        [DataMember]
        public SpendingCenterDTO SpendingCenter { get; set; }

        /// <summary>
        /// Identificador único de empresa
        /// </summary>
        [DataMember]
        public UserDTO User { get; set; }

        /// <summary>
        /// Descripción de departamento
        /// </summary>
        [DataMember]
        public string DepartmentDescription { get; set; }

        /// <summary>
        /// Estado del departamento: A (Activo), D (Desactivo)
        /// </summary>
        [DataMember]
        public string DepartmentStatus { get; set; }
    }
}
