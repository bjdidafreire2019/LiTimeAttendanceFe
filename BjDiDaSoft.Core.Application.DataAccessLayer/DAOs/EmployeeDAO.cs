using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using BjDiDaSoft.Core.Application.UniversalConnector.Core;
using BjDiDaSoft.Core.Application.UniversalConnector.Servers;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjDiDaSoft.Core.Application.DataAccessLayer.DAOs
{
    public class EmployeeDAO
    {
        #region Connection
        
        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion

        #region DropDownList

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="tableName"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetDropDownList(int companyId, string tableName, string valueMember, string displayMember)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var dropDownList = new List<DropDownDTO>();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, tableName, valueMember, displayMember };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetDropDownList", parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        DropDownDTO dropDown = new DropDownDTO
                        {
                            DisplayMember = row["DISPLAY"].ToString(),
                            ValueMember = row["VALUE"].ToString()
                        };
                        dropDownList.Add(dropDown);
                    }
                }
                else
                {
                    dropDownList = null;
                }

            }
            catch (Exception exception)
            {
                dropDownList = null;
            }
            finally
            {
                connector.Dispose();
            }
            return dropDownList; 
        }

        #endregion
        
        #region Employee

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveEmployee(EmployeeDTO employee, string operation)
        {
            DataTable dataTable = new DataTable();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            MessageDTO message = new MessageDTO();

            try
            {
                connector.BeginTransaction(); 

                //Todos los registros
                object[] parameters = new object[] { employee.EmployeeId, employee.Company.CompanyId, employee.Department.DepartmentId, 
                                                     employee.ContractType.ContractTypeId, employee.Charge.ChargeId, 
                                                     employee.Schedule.ScheduleId, employee.User.UserId,
                                                     employee.IdentificationType.IdentificationTypeId,
                                                     employee.Sex.SexId, employee.SectorId, employee.IdentificationNumber,
                                                     employee.EmployeeName, employee.EmployeeLastName, employee.ScheduleType,
                                                     employee.EmployeeFingerPrint, employee.EmployeeCardNumber, employee.EmployeeBirthDate,
                                                     employee.EmployeeSalary, employee.EmployeeEntryDate, employee.EmployeeModificationDate,
                                                     employee.EmployeeValidityStartDate, employee.EmployeeValidityEndDate,  
                                                     employee.EmployeeTruancy, employee.EmployeeUsedCard, employee.EmployeeStreetAddress,   
                                                     employee.EmployeeStatus, operation };
                //connector.ExecuteNonQuery(CommandType.StoredProcedure, "SaveEmployee", parameters);
                dataTable = connector.ExecuteDataTable(CommandType.StoredProcedure, "SaveEmployee", parameters);

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
        public List<EmployeeDTO> GetEmployees(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var employees = new List<EmployeeDTO>();
            
            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();


            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetEmployees", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        EmployeeDTO employee = new EmployeeDTO
                        {
                            Charge = new ChargeDTO { ChargeId = Convert.ToInt32(row["CHARGE_ID"]) },
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            ContractType = new ContractTypeDTO { ContractTypeId = Convert.ToInt32(row["CONTRACT_TYPE_ID"]) },
                            Department = new DepartmentDTO { DepartmentId = Convert.ToInt32(row["DEPARTMENT_ID"]), DepartmentDescription = row["DEPARTMENT_DESCRIPTION"].ToString() },
                            EmployeeBirthDate = Convert.ToDateTime(row["EMPLOYEE_BIRTH_DATE"]),
                            EmployeeCardNumber = row["EMPLOYEE_CARD_NUMBER"].ToString(),
                            EmployeeEntryDate = Convert.ToDateTime(row["EMPLOYEE_ENTRY_DATE"]),
                            EmployeeFingerPrint = row["EMPLOYEE_FINGERPRINT"].ToString(),
                            EmployeeFullName = row["EMPLOYEE_LAST_NAME"].ToString() + " " + row["EMPLOYEE_NAME"].ToString(),
                            EmployeeId = Convert.ToInt32(row["EMPLOYEE_ID"]),
                            EmployeeLastName = row["EMPLOYEE_LAST_NAME"].ToString(),
                            EmployeeModificationDate = Convert.ToDateTime(row["EMPLOYEE_MODIFICATION_DATE"]),
                            EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                            EmployeeSalary = Convert.ToDecimal(row["EMPLOYEE_SALARY"].ToString()),
                            EmployeeStatus = row["EMPLOYEE_STATUS"].ToString(),
                            EmployeeStreetAddress = row["EMPLOYEE_STREET_ADDRESS"] == DBNull.Value ? "" : row["EMPLOYEE_STREET_ADDRESS"].ToString(),
                            EmployeeTruancy = row["EMPLOYEE_TRUANCY"].ToString(),
                            EmployeeUsedCard = row["EMPLOYEE_USED_CARD"].ToString(),
                            EmployeeValidityEndDate = Convert.ToDateTime(row["EMPLOYEE_VALIDITY_END_DATE"]),
                            EmployeeValidityStartDate = Convert.ToDateTime(row["EMPLOYEE_VALIDITY_START_DATE"]),
                            IdentificationNumber = row["EMPLOYEE_IDNUMBER"].ToString(),
                            IdentificationType = new IdentificationTypeDTO { IdentificationTypeId = Convert.ToInt32(row["IDENTIFICATION_TYPE_ID"]) },
                            Schedule = new ScheduleDTO { ScheduleId = Convert.ToInt32(row["SCHEDULE_ID"]) },
                            ScheduleType = row["SCHEDULE_TYPE"].ToString(),
                            SectorId = row["SECTOR_ID"] == DBNull.Value ? -1 : Convert.ToInt32(row["SECTOR_ID"]),
                            Sex = new SexDTO { SexId = Convert.ToInt32(row["SEX_ID"]) }
                        };
                        employees.Add(employee);
                    }
                }
                else
                {
                    employees = null;
                }

            }
            catch (Exception exception)
            {
                employees = null;
            }
            finally
            {
                connector.Dispose();
            }
            return employees;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ScheduleEmployeeReaderDTO> GetEmployeeReaders(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var employeeReaders = new List<ScheduleEmployeeReaderDTO>();

            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int scheduleId = Convert.ToInt32(whereClause["scheduleId"]);
            int employeeId = Convert.ToInt32(whereClause["employeeId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, scheduleId, employeeId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetEmployeeReaders", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        ScheduleEmployeeReaderDTO employeeReader = new ScheduleEmployeeReaderDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            Employee = new EmployeeDTO { EmployeeId = Convert.ToInt32(row["EMPLOYEE_ID"]) },
                            Reader = new ReaderDTO
                            {
                                ReaderId = Convert.ToInt32(row["READER_ID"]),
                                ReaderName = row["READER_NAME"].ToString(),
                                ReaderSerialNumber = row["READER_SERIAL_NUMBER"].ToString(),
                                ReaderType = row["READER_TYPE"].ToString()
                            },
                            Schedule = new ScheduleDTO { ScheduleId = Convert.ToInt32(row["SCHEDULE_ID"]) },
                            ScheduleAccess = row["SCHEDULE_ACCESS"].ToString(),
                            ScheduleDataFrame = row["SCHEDULE_DATA_FRAME"].ToString(),
                            ScheduleEndTime = row["SCHEDULE_END_TIME"].ToString(),
                            ScheduleEndTimeIncome = row["SCHEDULE_END_TIME_INCOME"].ToString(),
                            ScheduleEndTimeLunch = row["SCHEDULE_END_TIME_LUNCH"].ToString(),
                            ScheduleEndTimeOutput = row["SCHEDULE_END_TIME_OUTPUT"].ToString(),
                            ScheduleLunchTime = row["SCHEDULE_LUNCH_TIME"].ToString(),
                            ScheduleStartTime = row["SCHEDULE_START_TIME"].ToString(),
                            ScheduleStartTimeIncome = row["SCHEDULE_START_TIME_INCOME"].ToString(),
                            ScheduleStartTimeLunch = row["SCHEDULE_START_TIME_LUNCH"].ToString(),
                            ScheduleStartTimeOutput = row["SCHEDULE_START_TIME_OUTPUT"].ToString(),
                            ZoneNumber = Convert.ToInt32(row["ZONE_NUMBER"])
                        };
                        employeeReaders.Add(employeeReader);
                    }
                }
                else
                {
                    employeeReaders = null;
                }

            }
            catch (Exception exception)
            {
                employeeReaders = null;
            }
            finally
            {
                connector.Dispose();
            }
            return employeeReaders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ReaderDTO> GetReadersByEmployee(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            DataSet dataSet = new DataSet();
            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            var readers = new List<ReaderDTO>();
            int companyId = Convert.ToInt32(whereClause["companyId"]);
            int scheduleId = Convert.ToInt32(whereClause["scheduleId"]);
            int employeeId = Convert.ToInt32(whereClause["employeeId"]);
            int pageSize = Convert.ToInt32(whereClause["pageSize"]);
            int pageNumber = Convert.ToInt32(whereClause["pageNumber"]);
            string sortColumn = whereClause["sortColumn"].ToString();
            string sortOrder = whereClause["sortOrder"].ToString();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { companyId, employeeId, scheduleId, pageSize, pageNumber, sortColumn, sortOrder };
                connector.FillDataSet(dataSet, CommandType.StoredProcedure, "GetReadersByEmployee", parameters);

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    dataTable = dataSet.Tables[0];

                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        ReaderDTO reader = new ReaderDTO
                        {
                            Company = new CompanyDTO { CompanyId = Convert.ToInt32(row["COMPANY_ID"]) },
                            ReaderId = Convert.ToInt32(row["READER_ID"]),
                            ReaderName = row["READER_NAME"].ToString(),
                            ReaderSerialNumber = row["READER_SERIAL_NUMBER"].ToString(),
                            ReaderStatus = row["READER_STATUS"].ToString(),
                            ReaderStatusComm = row["READER_STATUS_COMM"].ToString(),
                            ReaderType = row["READER_TYPE"].ToString()
                        };
                        readers.Add(reader);
                    }
                }
                else
                {
                    readers = null;
                }

            }
            catch (Exception exception)
            {
                readers = null;
            }
            finally
            {
                connector.Dispose();
            }
            return readers;
        }

        #endregion
    }
}
