using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BjDiDaSoft.Core.Application.TimeAttendance.Startup))]
namespace BjDiDaSoft.Core.Application.TimeAttendance
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
