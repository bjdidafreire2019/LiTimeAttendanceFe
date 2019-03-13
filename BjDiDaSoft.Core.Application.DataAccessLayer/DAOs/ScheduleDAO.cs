using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using BjDiDaSoft.Core.Application.UniversalConnector.Core;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DAOs
{
    public class ScheduleDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region Schedule

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveSchedule(ScheduleDTO schedule, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction(); 

                //Todos los registros
                object[] parameters = new object[] { schedule.ScheduleId, schedule.Company.CompanyId, schedule.User.UserId, 
                                                     schedule.Workday.WorkdayId, schedule.ScheduleShortName, 
                                                     schedule.ScheduleDescription, schedule.ScheduleStartHour, schedule.ScheduleEndHour,
                                                     schedule.ScheduleLunchHour, schedule.ScheduleIsNight, schedule.ScheduleAccess,
                                                     schedule.ScheduleOuterZone, schedule.ScheduleInnerZone, schedule.ScheduleLunchTime,
                                                     schedule.ScheduleOutputDelay, schedule.ScheduleEntryDelay,    
                                                     schedule.ScheduleStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveSchedule", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveSchedule", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    message.ErrorCode = row["ERROR_NUMBER"].ToString();
                    message.ErrorMessage = row["ERROR_DESCRIPTION"].ToString();
                }

                if (message.ErrorCode == "0")
                {
                    connector.Commit();
                }
                else
                {
                    connector.RollBack();
                }
            }
            catch (Exception exception)
            {
                message.ErrorCode = "-1";
                message.ErrorMessage = exception.ToString();
                connector.RollBack(); 
            }
            finally
            {
                connector.Dispose();
            }
            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ScheduleDTO> GetSchedules(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var schedules = new List<ScheduleDTO>();

            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();


            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetSchedules", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        ScheduleDTO schedule = new ScheduleDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            EndHour = row["END_HOUR"].ToString(),
                            LunchHour = row["LUNCH_HOUR"].ToString(),
                            ScheduleAccess = row["SCHEDULE_ACCESS"].ToString(),
                            ScheduleDescription = row["SCHEDULE_DESCRIPTION"].ToString(),
                            ScheduleEndHour = row["SCHEDULE_END_HOUR"].ToString(),
                            ScheduleEntryDelay = Convert.ToInt32(row["SCHEDULE_ENTRY_DELAY"]),
                            ScheduleId = Convert.ToInt32(row["SCHEDULE_ID"]),
                            ScheduleInnerZone = Convert.ToInt32(row["SCHEDULE_INNER_ZONE"]),
                            ScheduleIsNight = row["SCHEDULE_IS_NIGHT"].ToString(),
                            ScheduleLunchHour = row["SCHEDULE_LUNCH_HOUR"].ToString(),
                            ScheduleLunchTime = Convert.ToInt32(row["SCHEDULE_LUNCH_TIME"]),
                            ScheduleOuterZone = Convert.ToInt32(row["SCHEDULE_OUTER_ZONE"]),
                            ScheduleOutputDelay = Convert.ToInt32(row["SCHEDULE_OUTPUT_DELAY"]),
                            ScheduleShortName = row["SCHEDULE_SHORT_NAME"].ToString(),
                            ScheduleStartHour = row["SCHEDULE_START_HOUR"].ToString(),
                            ScheduleStatus = row["SCHEDULE_STATUS"].ToString(),
                            StartHour = row["START_HOUR"].ToString(),
                            Workday = new WorkdayDTO { WorkdayId = Convert.ToInt32(row["WORKDAY_ID"]) }
                        };
                        schedules.Add(schedule);
                    }
                }
                else
                {
                    schedules = null;
                }

            }
            catch (Exception exception)
            {
                schedules = null;
            }
            finally
            {
                connector.Dispose();
            }
            return schedules;
        }

        #endregion
    }
}
