using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class DepartmentModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string SpendingCenterId { get; set; }
        public string UserId { get; set; }
        public string DepartmentDescription { get; set; }
        public string DepartmentStatus { get; set; }
        public string Operation { get; set; }
    }
}