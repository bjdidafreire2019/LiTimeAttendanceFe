using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class WorkdayModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string WorkdayDescription { get; set; }
        public string WorkdayShortName { get; set; }
        public string WorkdayStatus { get; set; }
        public string Operation { get; set; }
    }
}