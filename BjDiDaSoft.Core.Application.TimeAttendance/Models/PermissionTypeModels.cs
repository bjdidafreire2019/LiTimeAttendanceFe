using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class PermissionTypeModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string PermissionTypeDescription { get; set; }
        public string PermissionTypeType { get; set; }
        public string PermissionTypeStatus { get; set; }
        public string Operation { get; set; }
    }
}