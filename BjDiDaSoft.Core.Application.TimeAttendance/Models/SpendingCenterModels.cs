using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class SpendingCenterModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string SpendingCenterDescription { get; set; }
        public string SpendingCenterStatus { get; set; }
        public string Operation { get; set; }
    }
}