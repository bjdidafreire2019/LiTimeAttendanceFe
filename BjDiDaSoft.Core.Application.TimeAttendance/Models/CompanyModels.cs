using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class CompanyModels
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyStatus { get; set; }
        public string Operation { get; set; }
    }
}