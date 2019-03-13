using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.DataAccessLayer.DAOs;
using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;


namespace BjDiDaSoft.Core.Application.DataAccessLayer
{
    public class DataAccess
    {
        #region DAOs

        CompanyDAO companyDao = new CompanyDAO();
        ChargeDAO chargeDao = new ChargeDAO();
        ContractTypeDAO contractTypeDao = new ContractTypeDAO();
        DepartmentDAO departmentDao = new DepartmentDAO();
        EmployeeDAO employeeDao = new EmployeeDAO();
        PermissionTypeDAO permissionTypeDao = new PermissionTypeDAO();
        PersonDAO personDao = new PersonDAO();
        ReaderDAO readerDao = new ReaderDAO();
        ScheduleDAO scheduleDao = new ScheduleDAO();
        SpendingCenterDAO spendingCenterDao = new SpendingCenterDAO();
        UserDAO userDao = new UserDAO();
        WorkdayDAO workdayDao = new WorkdayDAO();

        #endregion

        #region 3DEs

        TripleDesProvider provider = new TripleDesProvider();

        #endregion

        #region Company

        /// <summary>
        /// SaveCompany
        /// </summary>
        /// <param name="company"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveCompany(CompanyDTO company, string operation)
        {
            return companyDao.SaveCompany(company, operation);
        }

        /// <summary>
        /// GetCompanies
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<CompanyDTO> GetCompanies(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return companyDao.GetCompanies(whereClause, ref dataTable);
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
            return chargeDao.SaveCharge(charge, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ChargeDTO> GetCharges(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return chargeDao.GetCharges(whereClause, ref dataTable);
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
            return contractTypeDao.SaveContractType(contractType, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ContractTypeDTO> GetContractTypes(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return contractTypeDao.GetContractTypes(whereClause, ref dataTable);
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
            return departmentDao.SaveDepartment(department, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<DepartmentDTO> GetDepartments(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return departmentDao.GetDepartments(whereClause, ref dataTable);
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
            return employeeDao.GetDropDownList(companyId, tableName, valueMember, displayMember);
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
            return employeeDao.SaveEmployee(employee, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<EmployeeDTO> GetEmployees(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return employeeDao.GetEmployees(whereClause, ref dataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ScheduleEmployeeReaderDTO> GetEmployeeReaders(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return employeeDao.GetEmployeeReaders(whereClause, ref dataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ReaderDTO> GetReadersByEmployee(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return employeeDao.GetReadersByEmployee(whereClause, ref dataTable);
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
            return permissionTypeDao.SavePermissionType(permissionType, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<PermissionTypeDTO> GetPermissionTypes(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return permissionTypeDao.GetPermissionTypes(whereClause, ref dataTable);
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
            personDao.SavePerson(person, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<PersonDTO> GetPersons(int pageSize, int pageNumber, string sortColumn, string sortOrder, ref DataTable dataTable)
        {
            return personDao.GetPersons(pageSize, pageNumber, sortColumn, sortOrder, ref dataTable);
        }

        #endregion

        #region Reader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public MessageDTO SaveReader(ReaderDTO reader, string operation)
        {
            return readerDao.SaveReader(reader, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ReaderDTO> GetReaders(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return readerDao.GetReaders(whereClause, ref dataTable);
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
            return userDao.GetValidUser(userLogin, EncryptData(userPassword));
        }

        #endregion

        #region 3Des

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public string EncryptData(string encrypt)
        {
            return provider.EncryptString3DES(encrypt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decrypt"></param>
        /// <returns></returns>
        public string DecryptData(string decrypt)
        {
            return provider.DecryptString3DES(decrypt);
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
            return scheduleDao.SaveSchedule(schedule, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<ScheduleDTO> GetSchedules(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return scheduleDao.GetSchedules(whereClause, ref dataTable);
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
            return spendingCenterDao.SaveSpendingCenter(spendingCenter, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<SpendingCenterDTO> GetSpendingCenters(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return spendingCenterDao.GetSpendingCenters(whereClause, ref dataTable);
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
            return workdayDao.SaveWorkday(workday, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereClause"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<WorkdayDTO> GetWorkdays(Dictionary<string, string> whereClause, ref DataTable dataTable)
        {
            return workdayDao.GetWorkdays(whereClause, ref dataTable);
        }


        #endregion
    }
}
