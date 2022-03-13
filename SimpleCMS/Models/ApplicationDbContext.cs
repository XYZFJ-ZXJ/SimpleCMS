using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using NLog;

namespace SimpleCMS.Models
{
    /// <summary>
    /// 55页，此类类似于SSH和SSM中的ApplicationContext.xml配置文件，特别是Hibernate和mybatis配置文件部分，在本文件中
    /// 需要定义各个实体类的实体集。从而应用Models文件夹下定义的各个实体.
    /// *******本文件最终被67页底部的ASP.NET的全局应用程序类Global.asax使用,另外还需要在Startup.Auth.cs中注册到Owin,
    /// 并且需要在基础控制器中BaseController.cs中进一步配置（通过Owin获取这些对象），以便在其它控制器中更方便的获取和使用Identity的manager对象和数据库上下文对象
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
       ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //C#构造器的重载和继承(55)
        public ApplicationDbContext() : base("DefaultConnection")
        {

            Database.Log = Logger.Info;
        }

        //56页底添加，为了是Owin上下文能够通过get()方法返回一个数据库上下文
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //IdentityModels.cs定义的实体不需要在此处配置实体集，因为与Identity验证有关的实体都交给了Identity库(52)处理.
        //定义实体集，引用文章类别实体(Category.cs),60
        public DbSet<Category> Categories { get; set; }
        //61
        public DbSet<Content> Contents { get; set; }
        //62
        public DbSet<Media> Mediae { get; set; }
        //62
        public DbSet<Tag> Tags { get; set; }
        //88页B处加入
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}