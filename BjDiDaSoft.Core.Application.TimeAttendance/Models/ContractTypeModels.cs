using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class ContractTypeModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string ContractTypeDescription { get; set; }
        public string ContractTypeStatus { get; set; }
        public string Operation { get; set; }
    }
}