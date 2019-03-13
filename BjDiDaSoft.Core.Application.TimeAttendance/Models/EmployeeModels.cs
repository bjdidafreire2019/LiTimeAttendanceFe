using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class EmployeeModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string DepartmentId { get; set; }
        public string ContractTypeId { get; set; }
        public string ChargeId { get; set; }
        public string ScheduleId { get; set; }
        public string UserId { get; set; }
        public string IdentificationTypeId { get; set; }
        public string SexId { get; set; }
        public string SectorId { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ScheduleType { get; set; }
        public string FingerPrint { get; set; }
        public string CardNumber { get; set; }
        public string BirthDate { get; set; }
        public string Salary { get; set; }
        public string EntryDate { get; set; }
        public string ModificationDate { get; set; }
        public string ValidityStartDate { get; set; }
        public string ValidityEndDate { get; set; }
        public string Truancy { get; set; }
        public string UsedCard { get; set; }
        public string StreetAddress { get; set; }
        public string Status { get; set; }
        public string Operation { get; set; }
        public int[] DeleteRecords { get; set; }
        public int[] UpdateRecords { get; set; }
    }
}