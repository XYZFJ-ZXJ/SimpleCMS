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
    //ASP.NET��ȫ�����õ�ȫ��Ӧ�ó����࣬����ASP.NET�������Զ�����.
    //���ļ�����54ҳ��ApplicationDbContext.cs<���ݿ������������ļ�>�ͺ�����Ǩ�Ƴ�ʼ���ļ�Configuration.cs�������
    //EF�����ASP.NET��ܵ�����
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //67ҳ�ײ�����,ÿ��Ӧ�ó�����£�����ִ��һ��Application_Start()����

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}
