using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BjDiDaSoft.Core.Application.BusinessLayer;
using BjDiDaSoft.Core.Application.DataAccessLayer.DTOs;
using System.Data;
using System.Reflection;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Account
{
    public partial class LoginTA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Business business = new Business();
            MessageDTO message = new MessageDTO();

            try
            {
                //message = business.GetValidUser(txtUserName.Text, txtUserPassword.Text);
                message = business.GetValidUser(username.Value, password.Value);
                if (message.ErrorCode.Equals("OK"))
                {
                    MessageBox.Text = "Usuario validado";
                    UserDTO user = (UserDTO)message.ListObject.ToArray().GetValue(0);
                    RoleDTO role = user.Role;
                    EmployeeDTO employee = user.Employee;
                    CompanyDTO company = user.Company; 

                    Session["ROLE"] = role.RoleId;
                    Session["USER_ID"] = user.UserId;
                    Session["USERNAME"] = "Usuario: " + user.UserLogin;
                    Session["IS_SUPERVISOR"] = user.UserSupervisor;
                    Session["ROLE"] = role.RoleId;
                    Session["COMPANY_ID"] = company.CompanyId;
                    Session["COMPANY_NAME"] = company.CompanyName;
                    Session["VALIDUSER"] = "si";

                    //Response.Redirect("SiteTA.aspx", true);

                    //var emm = business.GetEmployeesJSon(10, 1, "EMPLOYEE_NAME", "ASC");

                    Session.Timeout = 10;
                    Session["SesionActiva"] = "cata";

                    Response.Redirect("../SiteTA.aspx", true);
                }
                else
                {
                    MessageBox.Text = message.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Text = ex.Message.ToString();
            }
            finally
            {
            }
        }


    }
}