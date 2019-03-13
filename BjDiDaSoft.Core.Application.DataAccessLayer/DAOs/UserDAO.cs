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
    public class UserDAO
    {
        #region Connection

        //string connectionString = "Data Source=LFREIRER\\SQLEXPRESS;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210;";
        //string connectionString = "Data Source=LFREIRE-PC;Initial Catalog=TimeAttendance;User Id=sa;Password=dida1210*;";
        string connectionString = ConfigurationManager.ConnectionStrings["TAConnection"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionTA"].ToString();

        #endregion
 
        /// <summary>
        /// Valida si el usuario que ingresa al sistema es válido 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public MessageDTO GetValidUser(string userLogin, string userPassword)
        {
            DataTable dataTable = new DataTable();
            MessageDTO message = new MessageDTO();

            IUniversalConnector connector = new UniversalConnectorImpl(ServerEnumType.SqlServer, connectionString);
            List<object> users = new List<object>();

            try
            {
                //Todos los registros
                object[] parameters = new object[] { userLogin, userPassword };
                connector.FillDataTable(dataTable, CommandType.StoredProcedure, "GetValidUser", parameters);

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserDTO user = new UserDTO
                        {
                            Company = new CompanyDTO
                            {
                                CompanyId = Convert.ToInt32(row["COMPANY_ID"]),
                                CompanyName = row["COMPANY_NAME"].ToString(),
                                CompanyShortName = row["COMPANY_SHORT_NAME"].ToString()
                            },
                            UserId = Convert.ToInt32(row["USR_ID"]),
                            Employee = new EmployeeDTO
                            {
                                EmployeeId = Convert.ToInt32(row["EMPLOYEE_ID"]),
                                IdentificationNumber = row["EMPLOYEE_IDNUMBER"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString()
                            },
                            Role = new RoleDTO { RoleId = Convert.ToInt32(row["ROLE_ID"]) },
                            UserLogin = row["USR_LOGIN"].ToString(),
                            UserPassword = row["USR_PASSWORD"].ToString(),
                            UserStatus = row["USR_STATUS"].ToString(),
                            UserSupervisor = row["USR_SUPERVISOR"].ToString()
                        };
                        users.Add(user);
                    }
                    message.ErrorCode = "OK";
                    message.ErrorMessage = "";
                    message.ListObject = users; 
                }
                else
                {
                    message.ErrorCode = "-1";
                    message.ErrorMessage = "Usuario y/o contraseña incorrecta";
                    message.ListObject = null; 
                }
                
            }
            catch (Exception exception)
            {
                message.ErrorCode = "-1";
                message.ErrorMessage = exception.Message.ToString();
                users = null;
                message.ListObject = null;
            }
            finally
            {
                connector.Dispose();
            }
            return message; 
        }

    }
}
