using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.DataAccessLayer;
using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using System.Data;

namespace BjDiDaSoft.Core.Application.BusinessLayer
{
    public class Business
    {
        #region DataAccessLayer

        DataAccess dataAccess = new DataAccess();

        #endregion

        #region Company

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveCompany(CompanyDTO company, string operation)
        {
            return dataAccess.SaveCompany(company, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetCompaniesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var companies = new List<CompanyDTO>();
            companies = dataAccess.GetCompanies(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          companies);
        }


        #endregion

        #region Charge

        /// <summary>
        /// 
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveCharge(ChargeDTO charge, string operation)
        {
            return dataAccess.SaveCharge(charge, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetChargesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var charges = new List<ChargeDTO>();
            charges = dataAccess.GetCharges(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          charges);
        }


        #endregion

        #region ContractType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveContractType(ContractTypeDTO contractType, string operation)
        {
            return dataAccess.SaveContractType(contractType, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetContractTypesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var contractTypes = new List<ContractTypeDTO>();
            contractTypes = dataAccess.GetContractTypes(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          contractTypes);
        }


        #endregion

        #region Department

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveDepartment(DepartmentDTO department, string operation)
        {
            return dataAccess.SaveDepartment(department, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetDepartmentsJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var departments = new List<DepartmentDTO>();
            departments = dataAccess.GetDepartments(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          departments);
        }


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
            return dataAccess.GetDropDownList(companyId, tableName, valueMember, displayMember);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetIdentificationType(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_IDENTIFICATION_TYPE", "IDENTIFICATION_TYPE_ID", "IDENTIFICATION_TYPE_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetSex(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_SEX", "SEX_ID", "SEX_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetContractType(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_CONTRACT_TYPE", "CONTRACT_TYPE_ID", "CONTRACT_TYPE_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetSpendingCenter(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_SPENDING_CENTER", "SPENDING_CENTER_ID", "SPENDING_CENTER_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetCharge(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_CHARGE", "CHARGE_ID", "CHARGE_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetDepartment(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_DEPARTMENT", "DEPARTMENT_ID", "DEPARTMENT_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetSchedule(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_SCHEDULE", "SCHEDULE_ID", "SCHEDULE_DESCRIPTION");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="tableName"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetWorkDay(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_WORKDAY", "WORKDAY_ID", "WORKDAY_DESCRIPTION");
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
            return dataAccess.SaveEmployee(employee, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetEmployeesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var employees = new List<EmployeeDTO>();
            employees = dataAccess.GetEmployees(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          employees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetEmployeeReadersJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var employeeReaders = new List<ScheduleEmployeeReaderDTO>();
            employeeReaders = dataAccess.GetEmployeeReaders(whereClause, ref dataTable);

            if (dataTable.Rows.Count > 0)
            {
                return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          employeeReaders);
            }
            else
            {
                return new JQGridJsonResponse(0, 0, 0, employeeReaders);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetReadersByEmployeeJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var readers = new List<ReaderDTO>();
            readers = dataAccess.GetReadersByEmployee(whereClause, ref dataTable);

            if (dataTable.Rows.Count > 0)
            {
                return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                              Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                              Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                              readers);
            }
            else
            {
                return new JQGridJsonResponse(0, 0, 0, readers);
            }
        }

        #endregion 

        #region PermissionType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionType"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SavePermissionType(PermissionTypeDTO permissionType, string operation)
        {
            return dataAccess.SavePermissionType(permissionType, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetPermissionTypesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var permissionTypes = new List<PermissionTypeDTO>();
            permissionTypes = dataAccess.GetPermissionTypes(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          permissionTypes);
        }


        #endregion


        #region Person

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <param name="operation"></param>
        public void SavePerson(PersonDTO person, string operation)
        {
            dataAccess.SavePerson(person, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetPersonsJSon(int pageSize, int pageNumber, string sortColumn, string sortOrder)
        {
            DataTable dataTable = new DataTable();
            var persons = new List<PersonDTO>();
            persons = dataAccess.GetPersons(pageSize, pageNumber, sortColumn, sortOrder, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          persons);

            /*
            return new JQGridJsonResponse(Convert.ToInt32(dataSet.Tables[0].Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataSet.Tables[0].Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataSet.Tables[0].Rows[0]["RecordCount"]),
                                          employees);*/
        }

        #endregion 

        #region Reader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveReader(ReaderDTO reader, string operation)
        {
            return dataAccess.SaveReader(reader, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetReadersJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var readers = new List<ReaderDTO>();
            readers = dataAccess.GetReaders(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          readers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DropDownDTO> GetReaderType(int companyId)
        {
            return dataAccess.GetDropDownList(companyId, "TA_READER_TYPE", "READER_TYPE_CD", "READER_TYPE_DESCRIPTION");
        }

        #endregion 

        #region User

        /// <summary>
        /// Valida si el usuario es válido 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public MessageDTO GetValidUser(string userLogin, string userPassword)
        {
            return dataAccess.GetValidUser(userLogin, userPassword);
        } 

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
            return dataAccess.SaveSchedule(schedule, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetSchedulesJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var schedules = new List<ScheduleDTO>();
            schedules = dataAccess.GetSchedules(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          schedules);
        }


        #endregion

        #region SpendingCenter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spendingCenter"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveSpendingCenter(SpendingCenterDTO spendingCenter, string operation)
        {
            return dataAccess.SaveSpendingCenter(spendingCenter, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetSpendingCentersJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var spendingCenters = new List<SpendingCenterDTO>();
            spendingCenters = dataAccess.GetSpendingCenters(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          spendingCenters);
        }


        #endregion

        #region Workday

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workday"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveWorkday(WorkdayDTO workday, string operation)
        {
            return dataAccess.SaveWorkday(workday, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public JQGridJsonResponse GetWorkdaysJSon(Dictionary<string, string> whereClause)
        {
            DataTable dataTable = new DataTable();
            var workdays = new List<WorkdayDTO>();
            workdays = dataAccess.GetWorkdays(whereClause, ref dataTable);

            return new JQGridJsonResponse(Convert.ToInt32(dataTable.Rows[0]["PageCount"]),
                                          Convert.ToInt32(dataTable.Rows[0]["CurrentPage"]),
                                          Convert.ToInt32(dataTable.Rows[0]["RecordCount"]),
                                          workdays);
        }


        #endregion
    }
}
