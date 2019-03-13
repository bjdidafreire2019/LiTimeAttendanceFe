using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class ChargeModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string ChargeDescription { get; set; }
        public string ChargeStatus { get; set; }
        public string Operation { get; set; }
    }
}