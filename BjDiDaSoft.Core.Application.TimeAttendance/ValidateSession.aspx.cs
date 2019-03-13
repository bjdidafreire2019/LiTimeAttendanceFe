using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BjDiDaSoft.Core.Application.TimeAttendance
{
    public partial class ValidateSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["datos"] = true;
        }

        [WebMethod()]
        public static bool KeepActiveSession()
        {
            if (HttpContext.Current.Session["datos"] != null)
                return true;
            else
                return false;
        }

        [WebMethod()]
        public static void SessionAbandon()
        {
            HttpContext.Current.Session.Remove("datos");
        }

    }
}