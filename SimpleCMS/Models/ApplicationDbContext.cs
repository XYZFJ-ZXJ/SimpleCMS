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
    /// 55页
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

        //56页底
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

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