using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BjDiDaSoft.Core.Application.TimeAttendance
{
    public class BasePage : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));

            if (Session["SesionActiva"] == null)
                Response.Redirect("Account/LoginTA.aspx");
        }

        public BasePage()
        {

        }

    }
}