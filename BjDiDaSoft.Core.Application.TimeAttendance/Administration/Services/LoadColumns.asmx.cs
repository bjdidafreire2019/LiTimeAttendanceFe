using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

using BjDiDaSoft.Core.Application.TimeAttendance.GridSettings;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Administration.Services
{
    /// <summary>
    /// Descripción breve de LoadColumns
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class LoadColumns : System.Web.Services.WebService
    {
        #region Charge

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetChargeColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="CHARGE_ID",index="CHARGE_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="CHARGE_DESCRIPTION",index="CHARGE_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="CHARGE_STATUS",index="CHARGE_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Company

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCompanyColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_SHORT_NAME",index="COMPANY_SHORT_NAME",label="Abreviatura",width=40,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_NAME",index="COMPANY_NAME",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_STATUS",index="COMPANY_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region ContractType

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetContractTypeColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="CONTRACT_TYPE_ID",index="CONTRACT_TYPE_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="CONTRACT_TYPE_DESCRIPTION",index="CONTRACT_TYPE_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="CONTRACT_TYPE_STATUS",index="CONTRACT_TYPE_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Department

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDepartmentColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="DEPARTMENT_ID",index="DEPARTMENT_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="DEPARTMENT_DESCRIPTION",index="DEPARTMENT_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SPENDING_CENTER_ID",index="SPENDING_CENTER_ID",label="SpendingCenterId",width=90,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SPENDING_CENTER_DESCRIPTION",index="SPENDING_CENTER_DESCRIPTION",label="Centro de Gasto",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="DEPARTMENT_STATUS",index="DEPARTMENT_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Employee

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetEmployeeColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="EMPLOYEE_ID",index="EMPLOYEE_ID",label="ID",width=25,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="DEPARTMENT_ID",index="DEPARTMENT_ID",label="Department Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="DEPARTMENT_DESCRIPTION",index="DEPARTMENT_DESCRIPTION",label="Departamento",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="CONTRACT_TYPE_ID",index="CONTRACT_TYPE_ID",label="Contract Type Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="CHARGE_ID",index="CHARGE_ID",label="Charge Id",width=40,align="left",editable=false,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_ID",index="SCHEDULE_ID",label="Schedule Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="IDENTIFICATION_TYPE_ID",index="IDENTIFICATION_TYPE_ID",label="Id. Type Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SEX_ID",index="SEX_ID",label="Sex Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SECTOR_ID",index="SECTOR_ID",label="Sector Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_IDNUMBER",index="EMPLOYEE_IDNUMBER",label="Cédula",width=30,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="EMPLOYEE_FULL_NAME",index="EMPLOYEE_FULL_NAME",label="Nombres",width=100,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="EMPLOYEE_LAST_NAME",index="EMPLOYEE_LAST_NAME",label="Apellidos",width=180,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_NAME",index="EMPLOYEE_NAME",label="Nombres",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_TYPE",index="SCHEDULE_TYPE",label="Schedule Type",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_FINGERPRINT",index="EMPLOYEE_FINGERPRINT",label="Huella",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_CARD_NUMBER",index="EMPLOYEE_CARD_NUMBER",label="Nro. Tarjeta",width=40,align="left",editable=false,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_BIRTH_DATE",index="EMPLOYEE_BIRTH_DATE",label="Fecha",width=25,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="EMPLOYEE_SALARY",index="EMPLOYEE_SALARY",label="Salario",width=180,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_ENTRY_DATE",index="EMPLOYEE_ENTRY_DATE",label="Fecha Ingreso",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_MODIFICATION_DATE",index="EMPLOYEE_MODIFICATION_DATE",label="Fecha Mod.",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_VALIDITY_START_DATE",index="EMPLOYEE_VALIDITY_START_DATE",label="Fecha Vigencia I",width=40,align="left",editable=false,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_VALIDITY_END_DATE",index="EMPLOYEE_VALIDITY_END_DATE",label="Fecha Vigencia F",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_TRUANCY",index="EMPLOYEE_TRUANCY",label="Falta",width=180,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_USED_CARD",index="EMPLOYEE_USED_CARD",label="Usa Tarjeta",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_STREET_ADDRESS",index="EMPLOYEE_STREET_ADDRESS",label="Dirección",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_STATUS",index="EMPLOYEE_STATUS",label="Estado",width=80,align="left",editable=true,editType="text",hidden=true}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetEmployeeReaderColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="READER_ID",index="READER_ID",label="ID",width=30,align="center",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Empresa",width=50,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_ID",index="SCHEDULE_ID",label="ScheduleId",width=50,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="EMPLOYEE_ID",index="EMPLOYEE_ID",label="EmployeeId",width=50,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="READER_SERIAL_NUMBER",index="READER_SERIAL_NUMBER",label="Número Serial",width=70,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="READER_NAME",index="READER_NAME",label="Nombre Lector",width=120,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="READER_TYPE",index="READER_TYPE",label="Tipo Lector",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="ZONE_NUMBER",index="ZONE_NUMBER",label="Nro. Zona",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_ACCESS",index="SCHEDULE_ACCESS",label="ScheduleAccess",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_DATA_FRAME",index="SCHEDULE_DATA_FRAME",label="SCHEDULE_DATA_FRAME",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_END_TIME",index="SCHEDULE_END_TIME",label="SCHEDULE_END_TIME",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_LUNCH_TIME",index="SCHEDULE_LUNCH_TIME",label="SCHEDULE_LUNCH_TIME",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_START_TIME",index="SCHEDULE_START_TIME",label="SCHEDULE_START_TIME",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_START_TIME_INCOME",index="SCHEDULE_START_TIME_INCOME",label="Desde",width=45,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_END_TIME_INCOME",index="SCHEDULE_END_TIME_INCOME",label="Hasta",width=45,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_START_TIME_LUNCH",index="SCHEDULE_START_TIME_LUNCH",label="Desde",width=45,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_END_TIME_LUNCH",index="SCHEDULE_END_TIME_LUNCH",label="Hasta",width=45,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_START_TIME_OUTPUT",index="SCHEDULE_START_TIME_OUTPUT",label="Desde",width=45,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_END_TIME_OUTPUT",index="SCHEDULE_END_TIME_OUTPUT",label="Hasta",width=45,align="center",editable=true,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region ContractType

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPermissionTypeColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="PERMISSION_TYPE_ID",index="PERMISSION_TYPE_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="PERMISSION_TYPE_DESCRIPTION",index="PERMISSION_TYPE_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="PERMISSION_TYPE_TYPE",index="PERMISSION_TYPE_TYPE",label="Permission Type",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="PERMISSION_TYPE_STATUS",index="PERMISSION_TYPE_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Person

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPersonColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="PERSON_ID",index="PERSON_ID",label="ID",width=40,align="left",editable=false,editType="text"},
                        new ColsModel{Name="PERSON_INUMBER",index="PERSON_INUMBER",label="Cédula",width=80,align="left",editable=true,editType="text"},
                        new ColsModel{Name="PERSON_NAME",index="PERSON_NAME",label="Nombre",width=180,align="left",editable=true,editType="text"},
                        new ColsModel{Name="PERSON_BIRTH_DATE",index="PERSON_BIRTH_DATE",label="Fecha",width=80,align="left",editable=true,editType="text"},
                        new ColsModel{Name="PERSON_WEIGHT",index="PERSON_WEIGHT",label="Peso",width=80,align="left",editable=true,editType="text"}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }


        #endregion

        #region Reader

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetReaderColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="READER_ID",index="READER_ID",label="ID",width=30,align="center",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Empresa",width=50,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="READER_SERIAL_NUMBER",index="READER_SERIAL_NUMBER",label="Número Serial",width=50,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="READER_NAME",index="READER_NAME",label="Nombre Lector",width=100,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="READER_TYPE",index="READER_TYPE",label="Tipo Lector",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="READER_STATUS_COMM",index="READER_STATUS_COMM",label="Estado Comm",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="READER_STATUS",index="READER_STATUS",label="Estado",width=80,align="left",editable=true,editType="text",hidden=true}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Schedule

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetScheduleColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="SCHEDULE_ID",index="SCHEDULE_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="WORKDAY_ID",index="WORKDAY_ID",label="WorkDay Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="WORKDAY_DESCRIPTION",index="WORKDAY_DESCRIPTION",label="Jornada Laboral",width=90,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_SHORT_NAME",index="SCHEDULE_SHORT_NAME",label="Nombre Corto",width=25,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_DESCRIPTION",index="SCHEDULE_DESCRIPTION",label="Descripción",width=90,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_START_HOUR",index="SCHEDULE_START_HOUR",label="Inicio Jornada",width=35,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="START_HOUR",index="START_HOUR",label="Inicio Jornada",width=35,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_LUNCH_HOUR",index="SCHEDULE_LUNCH_HOUR",label="Almuerzo/Merienda",width=35,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="LUNCH_HOUR",index="LUNCH_HOUR",label="Almuerzo/Merienda",width=35,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_END_HOUR",index="SCHEDULE_END_HOUR",label="Fin Jornada",width=35,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="END_HOUR",index="END_HOUR",label="Fin Jornada",width=35,align="center",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SCHEDULE_IS_NIGHT",index="SCHEDULE_IS_NIGHT",label="Sector Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_ACCESS",index="SCHEDULE_ACCESS",label="Cédula",width=30,align="left",editable=false,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_OUTER_ZONE",index="SCHEDULE_OUTER_ZONE",label="Nombres",width=100,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_INNER_ZONE",index="SCHEDULE_INNER_ZONE",label="Apellidos",width=180,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_LUNCH_TIME",index="SCHEDULE_LUNCH_TIME",label="Nombres",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_OUTPUT_DELAY",index="SCHEDULE_OUTPUT_DELAY",label="Schedule Type",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_ENTRY_DELAY",index="SCHEDULE_ENTRY_DELAY",label="Huella",width=80,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SCHEDULE_STATUS",index="SCHEDULE_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=true}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region SpendingCenter

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSpendingCenterColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="SPENDING_CENTER_ID",index="SPENDING_CENTER_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="SPENDING_CENTER_DESCRIPTION",index="SPENDING_CENTER_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="SPENDING_CENTER_STATUS",index="SPENDING_CENTER_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=true}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion

        #region Workday

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetWorkdayColumns()
        {
            List<ColsModel> cols = new List<ColsModel>{
                        new ColsModel{Name="WORKDAY_ID",index="WORKDAY_ID",label="ID",width=12,align="left",editable=false,editType="text",hidden=false},
                        new ColsModel{Name="COMPANY_ID",index="COMPANY_ID",label="Company Id",width=40,align="left",editable=true,editType="text",hidden=true},
                        new ColsModel{Name="WORKDAY_SHORT_NAME",index="WORKDAY_SHORT_NAME",label="Abreviación",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="WORKDAY_DESCRIPTION",index="WORKDAY_DESCRIPTION",label="Descripción",width=90,align="left",editable=true,editType="text",hidden=false},
                        new ColsModel{Name="WORKDAY_STATUS",index="WORKDAY_STATUS",label="Estado",width=40,align="left",editable=false,editType="text",hidden=false}
            };

            JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(cols);
            return sJSON;
        }

        #endregion
    }
}
