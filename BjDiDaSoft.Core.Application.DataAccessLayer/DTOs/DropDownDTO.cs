using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    /// <summary>
    /// Descripción breve de DropDownList
    /// </summary>
    [DataContract]
    public class DropDownDTO
    {
        // <summary>
        /// ValueMember
        /// </summary>
        [DataMember]
        public string ValueMember { get; set; }

        /// <summary>
        /// DisplayMember
        /// </summary>
        [DataMember]
        public string DisplayMember { get; set; }
    }
}
