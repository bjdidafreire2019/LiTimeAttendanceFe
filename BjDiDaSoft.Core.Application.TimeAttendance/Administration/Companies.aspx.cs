using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BjDiDaSoft.Core.Application.TimeAttendance.Administration
{
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["VALIDUSER"] == null)
                Response.Redirect("../Account/LoginTA.aspx");

            companyId.Value = "0";
            userId.Value = Session["USER_ID"].ToString();
        }
    }
}