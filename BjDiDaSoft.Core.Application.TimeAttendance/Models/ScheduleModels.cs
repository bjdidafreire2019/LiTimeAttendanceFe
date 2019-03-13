using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Models
{
    public class ScheduleModels
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string WorkdayId { get; set; }
        public string UserId { get; set; }
        public string ScheduleShortName { get; set; }
        public string ScheduleDescription { get; set; }
        public string ScheduleStartHour { get; set; }
        public string ScheduleEndHour { get; set; }
        public string ScheduleLunchHour { get; set; }
        public string ScheduleIsNight { get; set; }
        public string ScheduleAccess { get; set; }
        public string ScheduleOuterZone { get; set; }
        public string ScheduleInnerZone { get; set; }
        public string ScheduleLunchTime { get; set; }
        public string ScheduleOutputDelay { get; set; }
        public string ScheduleEntryDelay { get; set; }
        public string ScheduleStatus { get; set; }
        public string Operation { get; set; }
    }
}