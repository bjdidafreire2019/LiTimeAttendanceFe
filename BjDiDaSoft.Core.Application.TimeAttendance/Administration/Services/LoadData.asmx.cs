using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

using BjDiDaSoft.Core.Application.BusinessLayer;
using System.Web.Script.Serialization;
using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Administration.Services
{
    /// <summary>
    /// Descripción breve de LoadData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class LoadData : System.Web.Services.WebService
    {
        #region Business

        Business business = new Business();

        #endregion

        #region Charge

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetCharges(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetChargesJSon(whereClause);
        }

        #endregion

        #region Company

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetCompanies(int pageSize, int currentPage, string sortColumn, string sortOrder)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetCompaniesJSon(whereClause);
        }

        #endregion

        #region ContractType

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetContractTypes(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetContractTypesJSon(whereClause);
        }

        #endregion

        #region Department

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetDepartments(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetDepartmentsJSon(whereClause);
        }

        #endregion

        #region Employee

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetEmployees(int pageSize, int currentPage, string sortColumn, string sortOrder)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", "1");                        //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetEmployeesJSon(whereClause);
        }

        [WebMethod]
        public string BindIdentificationType()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            //dropdownList = business.GetDropDownList(1, "TA_IDENTIFICATION_TYPE", "IDENTIFICATION_TYPE_ID", "IDENTIFICATION_TYPE_DESCRIPTION");
            dropdownList = business.GetIdentificationType(1);
            
            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }


        [WebMethod]
        public string BindSex()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            //dropdownList = business.GetDropDownList(1, "TA_SEX", "SEX_ID", "SEX_DESCRIPTION");
            dropdownList = business.GetSex(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        public string BindContractType()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            //dropdownList = business.GetDropDownList(1, "TA_CONTRACT_TYPE", "CONTRACT_TYPE_ID", "CONTRACT_TYPE_DESCRIPTION");
            dropdownList = business.GetContractType(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        public string BindCharge()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            dropdownList = business.GetCharge(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        public string BindSpendingCenter()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            //dropdownList = business.GetDropDownList(1, "TA_SPENDING_CENTER", "SPENDING_CENTER_ID", "SPENDING_CENTER_DESCRIPTION");
            dropdownList = business.GetSpendingCenter(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        public string BindDepartment()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            dropdownList = business.GetDepartment(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        public string BindSchedule()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            dropdownList = business.GetSchedule(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetEmployeeReaders(int companyId, int scheduleId, int employeeId, int pageSize, int currentPage, string sortColumn, string sortOrder)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("scheduleId", scheduleId.ToString());     //Identificador único de horario
            whereClause.Add("employeeId", employeeId.ToString());     //Identificador único de empleado
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetEmployeeReadersJSon(whereClause);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetReadersByEmployee(int companyId, int scheduleId, int employeeId, int pageSize, int currentPage, string sortColumn, string sortOrder)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("scheduleId", scheduleId.ToString());     //Identificador único de horario
            whereClause.Add("employeeId", employeeId.ToString());     //Identificador único de empleado
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetReadersByEmployeeJSon(whereClause);
        }

        #endregion

        #region PermissionType

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetPermissionTypes(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetPermissionTypesJSon(whereClause);
        }

        #endregion

        #region Person

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetPersons(int pageSize, int currentPage, string sortColumn, string sortOrder)
        {
            return business.GetPersonsJSon(pageSize, currentPage, sortColumn, sortOrder);
        }

        #endregion

        #region Reader

        [WebMethod]
        public string BindReaderType(int companyId)
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            dropdownList = business.GetReaderType(companyId);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetReaders(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetReadersJSon(whereClause);
        }

        #endregion

        #region Schedule

        [WebMethod]
        public string BindWorkDay()
        {
            List<DropDownDTO> dropdownList = new List<DropDownDTO>();

            dropdownList = business.GetWorkDay(1);

            JavaScriptSerializer jscript = new JavaScriptSerializer();

            return jscript.Serialize(dropdownList);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetSchedules(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());                        //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetSchedulesJSon(whereClause);
        }

        #endregion

        #region SpendingCenter

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetSpendingCenters(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetSpendingCentersJSon(whereClause);
        }

        #endregion

        #region Workday

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JQGridJsonResponse GetWorkdays(int pageSize, int currentPage, string sortColumn, string sortOrder, int companyId)
        {
            Dictionary<string, string> whereClause = new Dictionary<string, string>();

            whereClause.Add("companyId", companyId.ToString());       //Identificador único de empresa
            whereClause.Add("pageSize", pageSize.ToString());         //Tamaño página
            whereClause.Add("pageNumber", currentPage.ToString());    //Página actual
            whereClause.Add("sortColumn", sortColumn);                //Columna para ordenar
            whereClause.Add("sortOrder", sortOrder);                  //Ordenamiento ascendente o descendente

            return business.GetWorkdaysJSon(whereClause);
        }

        #endregion
    }
}
