using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BjDiDaSoft.Core.Application.TimeAttendance
{
    public partial class SiteTA : BasePage // System.Web.UI.Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //Response.Write(Session.Timeout.ToString());
        }
    }
}