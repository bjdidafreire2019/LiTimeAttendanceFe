using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DTOs
{
    public class MessageDTO
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<object> ListObject { get; set; }
    }
}
