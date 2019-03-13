using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;

namespace BjDiDaSoft.Core.Application.BusinessLayer
{
    /// <summary>
    /// Descripción breve de JQGridJsonResponse
    /// </summary>
    public class JQGridJsonResponse
    {
        #region Passive attributes.

        private int _pageCount;
        private int _currentPage;
        private int _recordCount;
        private List<JQGridItem> _items;

        #endregion

        #region Properties

        /// <summary>
        /// Cantidad de páginas del JQGrid.
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        /// <summary>
        /// Página actual del JQGrid.
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }
        /// <summary>
        /// Cantidad total de elementos de la lista.
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        /// <summary>
        /// Lista de elementos del JQGrid.
        /// </summary>
        public List<JQGridItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        #endregion

        #region Active attributes

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<CompanyDTO> companies)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (CompanyDTO company in companies)
                _items.Add(new JQGridItem(company.CompanyId,
                                          new List<string> { company.CompanyId.ToString(), company.CompanyShortName, 
                                                             company.CompanyName, company.CompanyStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<ContractTypeDTO> contractTypes)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (ContractTypeDTO contractType in contractTypes)
                _items.Add(new JQGridItem(contractType.ContractTypeId,
                                          new List<string> { contractType.ContractTypeId.ToString(), contractType.Company.CompanyId.ToString(),  
                                                             contractType.ContractTypeDescription, contractType.ContractTypeStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<EmployeeDTO> employees)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (EmployeeDTO employee in employees)
                _items.Add(new JQGridItem(employee.EmployeeId,
                                          new List<string> { employee.EmployeeId.ToString(), employee.Company.CompanyId.ToString(), 
                                                             employee.Department.DepartmentId.ToString(), employee.ContractType.ContractTypeId.ToString(), 
                                                             employee.Charge.ChargeId.ToString(),
                                                             employee.Schedule.ScheduleId.ToString(), employee.IdentificationType.IdentificationTypeId.ToString(),
                                                             employee.Sex.SexId.ToString(), employee.SectorId.ToString(), 
                                                             employee.IdentificationNumber, employee.EmployeeFullName, employee.Department.DepartmentDescription, 
                                                             employee.EmployeeLastName, employee.EmployeeName, employee.ScheduleType,
                                                             employee.EmployeeFingerPrint, employee.EmployeeCardNumber,
                                                             //employee.EmployeeBirthDate.ToShortDateString(), 
                                                             employee.EmployeeBirthDate.ToString("yyyy-MM-dd"), 
                                                             employee.EmployeeSalary.ToString(),
                                                             employee.EmployeeEntryDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                                                             employee.EmployeeModificationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                             employee.EmployeeValidityStartDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                                                             employee.EmployeeValidityEndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                             employee.EmployeeTruancy, employee.EmployeeUsedCard, employee.EmployeeStreetAddress,
                                                             employee.EmployeeStatus 
                                          }));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="recordCount"></param>
        /// <param name="persons"></param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<PersonDTO> persons)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (PersonDTO person in persons)
                _items.Add(new JQGridItem(person.PersonId,
                                          new List<string> { person.PersonId.ToString(), person.IdentificationNumber, 
                                                             person.PersonName, person.PersonBirthDate.ToShortDateString(), 
                                                             person.PersonWeight.ToString(), person.PersonStatus }));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<ReaderDTO> readers)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (ReaderDTO reader in readers)
                _items.Add(new JQGridItem(reader.ReaderId,
                                          new List<string> { reader.ReaderId.ToString(), reader.Company.CompanyId.ToString(), 
                                                             reader.ReaderSerialNumber, reader.ReaderName, reader.ReaderType,  
                                                             reader.ReaderStatusComm, reader.ReaderStatus }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="recordCount"></param>
        /// <param name="readers"></param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<ScheduleEmployeeReaderDTO> readers)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();

            if (readers != null)
            {
                foreach (ScheduleEmployeeReaderDTO reader in readers)
                    _items.Add(new JQGridItem(reader.Reader.ReaderId,
                                              new List<string> { reader.Reader.ReaderId.ToString(), reader.Company.CompanyId.ToString(),
                                                             reader.Schedule.ScheduleId.ToString(), reader.Employee.EmployeeId.ToString(), 
                                                             reader.Reader.ReaderSerialNumber, reader.Reader.ReaderName, 
                                                             reader.Reader.ReaderType, reader.ZoneNumber.ToString(), 
                                                             reader.ScheduleAccess, reader.ScheduleDataFrame,
                                                             reader.ScheduleEndTime, reader.ScheduleLunchTime,
                                                             reader.ScheduleStartTime, 
                                                             reader.ScheduleStartTimeIncome, reader.ScheduleEndTimeIncome,
                                                             reader.ScheduleStartTimeLunch, reader.ScheduleEndTimeLunch,
                                                             reader.ScheduleStartTimeOutput, reader.ScheduleEndTimeOutput
                                          }));
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<ScheduleDTO> schedules)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (ScheduleDTO schedule in schedules)
                _items.Add(new JQGridItem(schedule.ScheduleId,
                                          new List<string> { schedule.ScheduleId.ToString(), schedule.Company.CompanyId.ToString(), 
                                                             schedule.Workday.WorkdayId.ToString(), schedule.Workday.WorkdayDescription, 
                                                             schedule.ScheduleShortName, schedule.ScheduleDescription,
                                                             schedule.ScheduleStartHour, schedule.StartHour, schedule.ScheduleLunchHour,
                                                             schedule.LunchHour, schedule.ScheduleEndHour, schedule.EndHour, schedule.ScheduleIsNight, 
                                                             schedule.ScheduleAccess, schedule.ScheduleOuterZone.ToString(),
                                                             schedule.ScheduleInnerZone.ToString(), schedule.ScheduleLunchTime.ToString(),
                                                             schedule.ScheduleOutputDelay.ToString(), schedule.ScheduleEntryDelay.ToString(), 
                                                             schedule.ScheduleStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<SpendingCenterDTO> spendingCenters)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (SpendingCenterDTO spendingCenter in spendingCenters)
                _items.Add(new JQGridItem(spendingCenter.SpendingCenterId,
                                          new List<string> { spendingCenter.SpendingCenterId.ToString(), spendingCenter.Company.CompanyId.ToString(), 
                                                             spendingCenter.SpendingCenterDescription, spendingCenter.SpendingCenterStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<ChargeDTO> charges)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (ChargeDTO charge in charges)
                _items.Add(new JQGridItem(charge.ChargeId,
                                          new List<string> { charge.ChargeId.ToString(), charge.Company.CompanyId.ToString(), 
                                                             charge.ChargeDescription, charge.ChargeStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<PermissionTypeDTO> permissionTypes)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (PermissionTypeDTO permissionType in permissionTypes)
                _items.Add(new JQGridItem(permissionType.PermissionTypeId,
                                          new List<string> { permissionType.PermissionTypeId.ToString(), permissionType.Company.CompanyId.ToString(), 
                                                             permissionType.PermissionTypeDescription, permissionType.PermissionTypeType, 
                                                             permissionType.PermissionTypeStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<DepartmentDTO> departments)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (DepartmentDTO department in departments)
                _items.Add(new JQGridItem(department.DepartmentId,
                                          new List<string> { department.DepartmentId.ToString(), department.Company.CompanyId.ToString(), 
                                                             department.DepartmentDescription, 
                                                             department.SpendingCenter.SpendingCenterId.ToString(), 
                                                             department.SpendingCenter.SpendingCenterDescription, 
                                                             department.DepartmentStatus
                                          }));

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
        public JQGridJsonResponse(int pageCount, int currentPage, int recordCount, List<WorkdayDTO> workdays)
        {
            _pageCount = pageCount;
            _currentPage = currentPage;
            _recordCount = recordCount;
            _items = new List<JQGridItem>();
            foreach (WorkdayDTO workday in workdays)
                _items.Add(new JQGridItem(workday.WorkdayId,
                                          new List<string> { workday.WorkdayId.ToString(), workday.Company.CompanyId.ToString(), 
                                                             workday.WorkdayShortName, workday.WorkdayDescription, 
                                                             workday.WorkdayStatus
                                          }));

        }

        #endregion  
    }
}
