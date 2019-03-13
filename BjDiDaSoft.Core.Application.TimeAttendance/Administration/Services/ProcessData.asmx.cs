using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

using BjDiDaSoft.Core.Application.BusinessLayer;
using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using BjDiDaSoft.Core.Application.TimeAttendance.Models;
using System.Web.Script.Serialization;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Administration.Services
{
    /// <summary>
    /// Descripción breve de ProcessData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ProcessData : System.Web.Services.WebService
    {
        #region Business

        Business business = new Business();

        #endregion

        #region Charges

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveCharge(ChargeModels chargeModel)
        {
            ChargeDTO charge = new ChargeDTO();
            MessageDTO message = new MessageDTO();

            charge.Company = new CompanyDTO { CompanyId = Convert.ToInt32(chargeModel.CompanyId) };
            charge.ChargeDescription = chargeModel.ChargeDescription;
            charge.ChargeId = Convert.ToInt32(chargeModel.Id);
            charge.ChargeStatus = chargeModel.ChargeStatus;
            charge.User = new UserDTO { UserId = Convert.ToInt32(chargeModel.UserId) };

            if (String.Compare(chargeModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveCharge(charge, "I");
            }
            else if (String.Compare(chargeModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveCharge(charge, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteCharge(int chargeId, int companyId, int userId)
        {
            ChargeDTO charge = new ChargeDTO();
            MessageDTO message = new MessageDTO();

            charge.Company = new CompanyDTO { CompanyId = companyId };
            charge.ChargeDescription = "";
            charge.ChargeId = chargeId;
            charge.ChargeStatus = "";
            charge.User = new UserDTO { UserId = userId };

            message = business.SaveCharge(charge, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Company

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveCompany(CompanyModels companyModel)
        {
            CompanyDTO company = new CompanyDTO();
            MessageDTO message = new MessageDTO();

            company.CompanyId = Convert.ToInt32(companyModel.Id);
            company.CompanyName = companyModel.CompanyDescription;
            company.CompanyShortName = companyModel.CompanyShortName;
            company.CompanyStatus = companyModel.CompanyStatus;
            company.User = new UserDTO { UserId = Convert.ToInt32(companyModel.UserId) };

            if (String.Compare(companyModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveCompany(company, "I");
            }
            else if (String.Compare(companyModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveCompany(company, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteCompany(int companyId, int userId)
        {
            CompanyDTO company = new CompanyDTO();
            MessageDTO message = new MessageDTO();

            company.CompanyId = companyId;
            company.CompanyName = "";
            company.CompanyShortName = "";
            company.CompanyStatus = "";
            company.User = new UserDTO { UserId = userId };

            message = business.SaveCompany(company, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region ContractType

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveContractType(ContractTypeModels contractTypeModel)
        {
            ContractTypeDTO contractType = new ContractTypeDTO();
            MessageDTO message = new MessageDTO();

            contractType.Company = new CompanyDTO { CompanyId = Convert.ToInt32(contractTypeModel.CompanyId) };
            contractType.User = new UserDTO { UserId = Convert.ToInt32(contractTypeModel.UserId) };
            contractType.ContractTypeDescription = contractTypeModel.ContractTypeDescription;
            contractType.ContractTypeId = Convert.ToInt32(contractTypeModel.Id);
            contractType.ContractTypeStatus = contractTypeModel.ContractTypeStatus;

            if (String.Compare(contractTypeModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveContractType(contractType, "I");
            }
            else if (String.Compare(contractTypeModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveContractType(contractType, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteContractType(int contractTypeId, int companyId, int userId)
        {
            ContractTypeDTO contractType = new ContractTypeDTO();
            MessageDTO message = new MessageDTO();

            contractType.Company = new CompanyDTO { CompanyId = companyId };
            contractType.User = new UserDTO { UserId = userId };
            contractType.ContractTypeDescription = "";
            contractType.ContractTypeId = contractTypeId;
            contractType.ContractTypeStatus = "";

            message = business.SaveContractType(contractType, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Department

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveDepartment(DepartmentModels departmentModel)
        {
            DepartmentDTO department = new DepartmentDTO();
            MessageDTO message = new MessageDTO();

            department.Company = new CompanyDTO { CompanyId = Convert.ToInt32(departmentModel.CompanyId) };
            department.DepartmentDescription = departmentModel.DepartmentDescription;
            department.DepartmentId = Convert.ToInt32(departmentModel.Id);
            department.DepartmentStatus = departmentModel.DepartmentStatus;
            department.SpendingCenter = new SpendingCenterDTO { SpendingCenterId = Convert.ToInt32(departmentModel.SpendingCenterId) };
            department.User = new UserDTO { UserId = Convert.ToInt32(departmentModel.UserId) };

            if (String.Compare(departmentModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveDepartment(department, "I");
            }
            else if (String.Compare(departmentModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveDepartment(department, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteDepartment(int departmentId, int companyId, int spendingCenterId, int userId)
        {
            DepartmentDTO department = new DepartmentDTO();
            MessageDTO message = new MessageDTO();

            department.Company = new CompanyDTO { CompanyId = companyId };
            department.DepartmentDescription = "";
            department.DepartmentId = departmentId;
            department.DepartmentStatus = "";
            department.SpendingCenter = new SpendingCenterDTO { SpendingCenterId = spendingCenterId, SpendingCenterDescription = "" };
            department.User = new UserDTO { UserId = userId };

            message = business.SaveDepartment(department, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Employee

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveEmployee(EmployeeModels employeeModel)
        {
            EmployeeDTO employee = new EmployeeDTO();
            MessageDTO message = new MessageDTO();

            employee.Charge = new ChargeDTO { ChargeId = Convert.ToInt32(employeeModel.ChargeId) };
            employee.Company = new CompanyDTO { CompanyId = Convert.ToInt32(employeeModel.CompanyId) };
            employee.ContractType = new ContractTypeDTO { ContractTypeId = Convert.ToInt32(employeeModel.ContractTypeId) };
            employee.Department = new DepartmentDTO { DepartmentId = Convert.ToInt32(employeeModel.DepartmentId) };
            employee.EmployeeBirthDate = Convert.ToDateTime(DateFormat(employeeModel.BirthDate));
            employee.EmployeeCardNumber = employeeModel.CardNumber;
            employee.EmployeeEntryDate = Convert.ToDateTime(DateTime.Now);
            employee.EmployeeFingerPrint = employeeModel.FingerPrint;
            employee.EmployeeLastName = employeeModel.LastName;
            employee.EmployeeModificationDate = DateTime.Now;
            employee.EmployeeName = employeeModel.Name;
            employee.EmployeeSalary = Convert.ToDecimal(employeeModel.Salary);
            employee.EmployeeStatus = employeeModel.Status;
            employee.EmployeeStreetAddress = employeeModel.StreetAddress;
            employee.EmployeeTruancy = employeeModel.Truancy;
            employee.EmployeeUsedCard = employeeModel.UsedCard;
            employee.EmployeeValidityEndDate = Convert.ToDateTime(DateFormat(employeeModel.ValidityEndDate));
            employee.EmployeeValidityStartDate = Convert.ToDateTime(DateFormat(employeeModel.ValidityStartDate));
            employee.EmployeeId = Convert.ToInt32(employeeModel.Id);
            employee.IdentificationNumber = employeeModel.IdentificationNumber;
            employee.IdentificationType = new IdentificationTypeDTO { IdentificationTypeId = Convert.ToInt32(employeeModel.IdentificationTypeId) };
            employee.Schedule = new ScheduleDTO { ScheduleId = Convert.ToInt32(employeeModel.ScheduleId) };
            employee.ScheduleType = employeeModel.ScheduleType;
            employee.SectorId = Convert.ToInt32(employeeModel.SectorId);
            employee.Sex = new SexDTO { SexId = Convert.ToInt32(employeeModel.SexId) };
            employee.User = new UserDTO { UserId = Convert.ToInt32(employeeModel.UserId) };

            if (String.Compare(employeeModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveEmployee(employee, "I");
            }
            else if (String.Compare(employeeModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveEmployee(employee, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        [WebMethod]
        public string SaveEmployee2(EmployeeModels employeeModel)
        {
            var data = new { Greeting = "Hello", Name = "Luis" + " " + "Freire" };

            // We are using an anonymous object above, but we could use a typed one too (SayHello class is defined below)
            // SayHello data = new SayHello { Greeting = "Hello", Name = firstName + " " + lastName };

            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize(data);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteEmployee(int employeeId, int companyId, int userId)
        {
            EmployeeDTO employee = new EmployeeDTO();
            MessageDTO message = new MessageDTO();

            employee.Charge = new ChargeDTO { ChargeId = -1 };
            employee.Company = new CompanyDTO { CompanyId = companyId };
            employee.ContractType = new ContractTypeDTO { ContractTypeId = -1 };
            employee.Department = new DepartmentDTO { DepartmentId = -1 };
            employee.EmployeeBirthDate = Convert.ToDateTime(DateTime.Now);
            employee.EmployeeCardNumber = "";
            employee.EmployeeEntryDate = Convert.ToDateTime(DateTime.Now);
            employee.EmployeeFingerPrint = "";
            employee.EmployeeLastName = "";
            employee.EmployeeModificationDate = DateTime.Now;
            employee.EmployeeName = "";
            employee.EmployeeSalary = 0;
            employee.EmployeeStatus = "";
            employee.EmployeeStreetAddress = "";
            employee.EmployeeTruancy = "";
            employee.EmployeeUsedCard = "";
            employee.EmployeeValidityEndDate = Convert.ToDateTime(DateTime.Now);
            employee.EmployeeValidityStartDate = Convert.ToDateTime(DateTime.Now);
            employee.EmployeeId = employeeId;
            employee.IdentificationNumber = "";
            employee.IdentificationType = new IdentificationTypeDTO { IdentificationTypeId = -1 };
            employee.Schedule = new ScheduleDTO { ScheduleId = -1 };
            employee.ScheduleType = "";
            employee.SectorId = -1;
            employee.Sex = new SexDTO { SexId = -1 };
            employee.User = new UserDTO { UserId = userId };

            message = business.SaveEmployee(employee, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region PermissionType

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SavePermissionType(PermissionTypeModels permissionTypeModel)
        {
            PermissionTypeDTO permissionType = new PermissionTypeDTO();
            MessageDTO message = new MessageDTO();

            permissionType.Company = new CompanyDTO { CompanyId = Convert.ToInt32(permissionTypeModel.CompanyId) };
            permissionType.User = new UserDTO { UserId = Convert.ToInt32(permissionTypeModel.UserId) };
            permissionType.PermissionTypeDescription = permissionTypeModel.PermissionTypeDescription;
            permissionType.PermissionTypeId = Convert.ToInt32(permissionTypeModel.Id);
            permissionType.PermissionTypeType = permissionTypeModel.PermissionTypeType;
            permissionType.PermissionTypeStatus = permissionTypeModel.PermissionTypeStatus;

            if (String.Compare(permissionTypeModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SavePermissionType(permissionType, "I");
            }
            else if (String.Compare(permissionTypeModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SavePermissionType(permissionType, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeletePermissionType(int permissionTypeId, int companyId, int userId)
        {
            PermissionTypeDTO permissionType = new PermissionTypeDTO();
            MessageDTO message = new MessageDTO();

            permissionType.Company = new CompanyDTO { CompanyId = companyId };
            permissionType.User = new UserDTO { UserId = userId };
            permissionType.PermissionTypeDescription = "";
            permissionType.PermissionTypeId = permissionTypeId;
            permissionType.PermissionTypeType = "";
            permissionType.PermissionTypeStatus = "";

            message = business.SavePermissionType(permissionType, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Person

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteData(string id, string oper)
        {
            PersonDTO person = new PersonDTO();

            if (String.Compare(oper, "del", StringComparison.Ordinal) == 0)
            {
                string[] ids = id.Split(',');

                foreach (string employeeId in ids)
                {
                    person.PersonId = Convert.ToInt32(employeeId);
                    person.PersonBirthDate = Convert.ToDateTime(DateTime.Now);
                    person.PersonName = "";
                    person.PersonStatus = "A";
                    person.PersonWeight = 0;
                    person.IdentificationNumber = "";

                    business.SavePerson(person, "D");
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EditData(string PERSON_INUMBER, string PERSON_NAME, string PERSON_BIRTH_DATE, string PERSON_WEIGHT, string id, string oper)
        {
            PersonDTO person = new PersonDTO();


            person.PersonBirthDate = Convert.ToDateTime(PERSON_BIRTH_DATE);
            person.PersonName = PERSON_NAME;
            person.PersonStatus = "A";
            person.PersonWeight = Convert.ToDecimal(PERSON_WEIGHT);
            person.IdentificationNumber = PERSON_INUMBER;

            if (String.Compare(id, "_empty", StringComparison.Ordinal) == 0 ||
                String.Compare(oper, "add", StringComparison.Ordinal) == 0)
            {
                person.PersonId = 0;
                business.SavePerson(person, "I");
            }
            else if (String.Compare(oper, "edit", StringComparison.Ordinal) == 0)
            {
                person.PersonId = Convert.ToInt32(id);
                business.SavePerson(person, "U");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SavePerson(EmployeeModels employeeModel)
        {
            PersonDTO person = new PersonDTO();

            person.PersonBirthDate = Convert.ToDateTime(employeeModel.BirthDate);
            person.PersonName = employeeModel.Name;
            person.PersonStatus = "A";
            person.PersonWeight = Convert.ToDecimal(employeeModel.Salary);
            person.IdentificationNumber = employeeModel.IdentificationNumber;

            if (String.Compare(employeeModel.Id, "_empty", StringComparison.Ordinal) == 0 ||
                String.Compare(employeeModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                person.PersonId = 0;
                business.SavePerson(person, "I");
            }
            else if (String.Compare(employeeModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                person.PersonId = Convert.ToInt32(employeeModel.Id);
                business.SavePerson(person, "U");
            }
        }

        #endregion

        #region Reader

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveReader(ReaderModels readerModel)
        {
            ReaderDTO reader = new ReaderDTO();
            MessageDTO message = new MessageDTO();

            reader.Company = new CompanyDTO { CompanyId = Convert.ToInt32(readerModel.CompanyId) };
            reader.User = new UserDTO { UserId = Convert.ToInt32(readerModel.UserId) };
            reader.ReaderId = Convert.ToInt32(readerModel.Id);
            reader.ReaderName = readerModel.ReaderDescription;
            reader.ReaderSerialNumber = readerModel.ReaderSerialNumber;
            reader.ReaderStatus = readerModel.ReaderStatus;
            reader.ReaderStatusComm = readerModel.ReaderStatusComm;

            if (String.Compare(readerModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveReader(reader, "I");
            }
            else if (String.Compare(readerModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveReader(reader, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteReader(int readerId, int companyId, int userId)
        {
            ReaderDTO reader = new ReaderDTO();
            MessageDTO message = new MessageDTO();

            reader.Company = new CompanyDTO { CompanyId = companyId };
            reader.User = new UserDTO { UserId = userId };
            reader.ReaderId = readerId;
            reader.ReaderName = "";
            reader.ReaderSerialNumber = "";
            reader.ReaderStatus = "";
            reader.ReaderStatusComm = "";

            message = business.SaveReader(reader, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Schedule

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveSchedule(ScheduleModels scheduleModel)
        {
            ScheduleDTO schedule = new ScheduleDTO();
            MessageDTO message = new MessageDTO();

            schedule.Company = new CompanyDTO { CompanyId = Convert.ToInt32(scheduleModel.CompanyId) };
            schedule.ScheduleAccess = scheduleModel.ScheduleAccess;
            schedule.ScheduleDescription = scheduleModel.ScheduleDescription;
            schedule.ScheduleEndHour = scheduleModel.ScheduleEndHour;
            schedule.ScheduleEntryDelay = Convert.ToInt32(scheduleModel.ScheduleEntryDelay);
            schedule.ScheduleId = Convert.ToInt32(scheduleModel.Id);
            schedule.ScheduleInnerZone = Convert.ToInt32(scheduleModel.ScheduleInnerZone);
            schedule.ScheduleIsNight = scheduleModel.ScheduleIsNight;
            schedule.ScheduleLunchHour = scheduleModel.ScheduleLunchHour;
            schedule.ScheduleLunchTime = Convert.ToInt32(scheduleModel.ScheduleLunchTime);
            schedule.ScheduleOuterZone = Convert.ToInt32(scheduleModel.ScheduleOuterZone);
            schedule.ScheduleOutputDelay = Convert.ToInt32(scheduleModel.ScheduleOutputDelay);
            schedule.ScheduleShortName = scheduleModel.ScheduleShortName;
            schedule.ScheduleStartHour = scheduleModel.ScheduleStartHour;
            schedule.ScheduleStatus = scheduleModel.ScheduleStatus;
            schedule.User = new UserDTO { UserId = Convert.ToInt32(scheduleModel.UserId) };
            schedule.Workday = new WorkdayDTO { WorkdayId = Convert.ToInt32(scheduleModel.WorkdayId) };

            if (String.Compare(scheduleModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveSchedule(schedule, "I");
            }
            else if (String.Compare(scheduleModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveSchedule(schedule, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteSchedule(int scheduleId, int companyId, int userId)
        {
            ScheduleDTO schedule = new ScheduleDTO();
            MessageDTO message = new MessageDTO();

            schedule.Company = new CompanyDTO { CompanyId = companyId };
            schedule.ScheduleAccess = "";
            schedule.ScheduleDescription = "";
            schedule.ScheduleEndHour = DateTime.Now.ToString("dd/MM/yyyy");
            schedule.ScheduleEntryDelay = -1;
            schedule.ScheduleId = scheduleId;
            schedule.ScheduleInnerZone = -1;
            schedule.ScheduleIsNight = "";
            schedule.ScheduleLunchHour = DateTime.Now.ToString("dd/MM/yyyy");
            schedule.ScheduleLunchTime = -1;
            schedule.ScheduleOuterZone = -1;
            schedule.ScheduleOutputDelay = -1;
            schedule.ScheduleShortName = "";
            schedule.ScheduleStartHour = DateTime.Now.ToString("dd/MM/yyyy");
            schedule.ScheduleStatus = "";
            schedule.User = new UserDTO { UserId = userId };
            schedule.Workday = new WorkdayDTO { WorkdayId = -1 };

            message = business.SaveSchedule(schedule, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region SpendingCenter

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveSpendingCenter(SpendingCenterModels spendingCenterModel)
        {
            SpendingCenterDTO spendingCenter = new SpendingCenterDTO();
            MessageDTO message = new MessageDTO();

            spendingCenter.Company = new CompanyDTO { CompanyId = Convert.ToInt32(spendingCenterModel.CompanyId) };
            spendingCenter.SpendingCenterDescription = spendingCenterModel.SpendingCenterDescription;
            spendingCenter.SpendingCenterId = Convert.ToInt32(spendingCenterModel.Id);
            spendingCenter.SpendingCenterStatus = spendingCenterModel.SpendingCenterStatus;
            spendingCenter.User = new UserDTO { UserId = Convert.ToInt32(spendingCenterModel.UserId) };

            if (String.Compare(spendingCenterModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveSpendingCenter(spendingCenter, "I");
            }
            else if (String.Compare(spendingCenterModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveSpendingCenter(spendingCenter, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteSpendingCenter(int spendingCenterId, int companyId, int userId)
        {
            SpendingCenterDTO spendingCenter = new SpendingCenterDTO();
            MessageDTO message = new MessageDTO();

            spendingCenter.Company = new CompanyDTO { CompanyId = companyId };
            spendingCenter.SpendingCenterDescription = "";
            spendingCenter.SpendingCenterId = spendingCenterId;
            spendingCenter.SpendingCenterStatus = "";
            spendingCenter.User = new UserDTO { UserId = userId };

            message = business.SaveSpendingCenter(spendingCenter, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region Workday

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveWorkday(WorkdayModels workdayModel)
        {
            WorkdayDTO workday = new WorkdayDTO();
            MessageDTO message = new MessageDTO();

            workday.Company = new CompanyDTO { CompanyId = Convert.ToInt32(workdayModel.CompanyId) };
            workday.User = new UserDTO { UserId = Convert.ToInt32(workdayModel.UserId) };
            workday.WorkdayDescription = workdayModel.WorkdayDescription;
            workday.WorkdayId = Convert.ToInt32(workdayModel.Id);
            workday.WorkdayShortName = workdayModel.WorkdayShortName;
            workday.WorkdayStatus = workdayModel.WorkdayStatus;

            if (String.Compare(workdayModel.Operation, "add", StringComparison.Ordinal) == 0)
            {
                message = business.SaveWorkday(workday, "I");
            }
            else if (String.Compare(workdayModel.Operation, "edit", StringComparison.Ordinal) == 0)
            {
                message = business.SaveWorkday(workday, "U");
            }

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteWorkday(int workdayId, int companyId, int userId)
        {
            WorkdayDTO workday = new WorkdayDTO();
            MessageDTO message = new MessageDTO();

            workday.Company = new CompanyDTO { CompanyId = companyId };
            workday.User = new UserDTO { UserId = userId };
            workday.WorkdayDescription = "";
            workday.WorkdayId = workdayId;
            workday.WorkdayShortName = "";
            workday.WorkdayStatus = "";

            message = business.SaveWorkday(workday, "D");

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(message);
            return sJSON;
        }

        #endregion

        #region privates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string DateFormat(string date)
        {
            string dateFormat = "";
            IFormatProvider culture;
            DateTime dateTime;

            try
            {
                culture = new System.Globalization.CultureInfo("en-US", true);
                dateTime = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                dateFormat = dateTime.ToString("dd/MM/yyyy");
            }
            catch(Exception ex){
                culture = new System.Globalization.CultureInfo("es-ES", true);
                dateTime = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                dateFormat = dateTime.ToString("dd/MM/yyyy");
            }
            return dateFormat; 
        }

        #endregion
    }
}
