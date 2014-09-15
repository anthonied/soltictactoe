using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicTacToe.Web.Startup))]
namespace TicTacToe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
