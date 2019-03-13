using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class ReaderModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string ReaderSerialNumber { get; set; }
        public string ReaderDescription { get; set; }
        public string ReaderType { get; set; }
        public string ReaderStatusComm { get; set; }
        public string ReaderStatus { get; set; }
        public string Operation { get; set; }
    }
}