using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleCMS.Startup))]
namespace SimpleCMS
{
    public partial class Startup
    {
        //56页，是通过导入其它项目文件得来的
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
