using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleCMS.Migrations;
using SimpleCMS.Models;

namespace SimpleCMS
{
    //ASP.NET的全局配置的全局应用程序类，创建ASP.NET程序是自动产生.
    //本文件调用54页的ApplicationDbContext.cs<数据库上下文配置文件>和和数据迁移初始化文件Configuration.cs最终完成
    //EF框架与ASP.NET框架的整合
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //67页底部加入,每当应用程序更新，都会执行一次Application_Start()方法

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
