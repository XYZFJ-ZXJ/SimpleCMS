using Microsoft.AspNet.Identity;
using SimpleCMS.Models;

//本文件是EF框架Code First模式启动数据迁移自动生成的文件<相当于EF框架的配置文件>，可以在里面对迁移做一些配置和调整，
//还有设置数据库自增，添加触发器,进一步完善表，一些实体的初始化(一版Ientity验证所需的实体比如用户实体，角色实体)，配置自动迁移等操作.
//*******本文件最终被67页底部的ASP.NET的全局应用程序类Global.asax使用
namespace SimpleCMS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleCMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //63页
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SimpleCMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //64页，调整文章分类表的自增种子值.
            if (!context.Categories.Any())
            {
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('T_Category', RESEED, 10000)");
            }

            //64-65使用触发器：64-65
            context.Database.ExecuteSqlCommand(@"
                IF OBJECT_ID(N'trg_CategoryInsert', N'TR') IS NOT NULL  
               DROP TRIGGER trg_CategoryInsert;  
            ");

            context.Database.ExecuteSqlCommand(@"
                CREATE TRIGGER [dbo].[trg_CategoryInsert] 
                ON  [dbo].[T_Category]
                FOR INSERT
                AS 
                BEGIN
                  DECLARE @numrows int
                  SET @numrows = @@ROWCOUNT
                  if @numrows > 1
                    BEGIN
                    RAISERROR('只支持单行插入。', 16, 1)
                    ROLLBACK TRAN
                  END
                  ELSE
                  BEGIN
                  UPDATE
                    E
                  SET
                    HierarchyLevel    =
                  CASE
                    WHEN E.ParentId IS NULL THEN 0
                    ELSE Parent.HierarchyLevel + 1
                  END,
                  FullPath =
                  CASE
                    WHEN E.ParentId IS NULL THEN '.'
                    ELSE Parent.FullPath
                    END + CAST(E.Id AS nvarchar(10)) + '.'
                    FROM
                       T_Category AS E
                    INNER JOIN
                       inserted AS I ON I.Id = E.Id
                    LEFT OUTER JOIN
                       T_Category AS Parent ON Parent.Id = E.ParentId
                  END
                END
            ");

            //65-66
            context.Database.ExecuteSqlCommand(@"
               IF OBJECT_ID(N'trg_CategoryUpdate', N'TR') IS NOT NULL  
               DROP TRIGGER trg_CategoryUpdate;  
            ");

            context.Database.ExecuteSqlCommand(@"
               CREATE TRIGGER [dbo].[trg_CategoryUpdate]
               ON  [dbo].[T_Category]
               FOR Update
               AS 
               BEGIN
                IF @@ROWCOUNT = 0
                RETURN
                if UPDATE(ParentId)
                BEGIN
                 UPDATE
                 E
                 SET
                 HierarchyLevel    =
                    E.HierarchyLevel - I.HierarchyLevel +
                        CASE
                            WHEN I.ParentId IS NULL THEN 0
                            ELSE Parent.HierarchyLevel + 1
                        END,
                    FullPath =
                        ISNULL(Parent.FullPath, '.') +
                        CAST(I.Id as nvarchar(10)) + '.' +
                        RIGHT(E.FullPath, len(E.FullPath) - len(I.FullPath))
                  FROM
                    T_Category AS E
                  INNER JOIN
                    inserted AS I ON E.FullPath LIKE I.FullPath + '%'
                  LEFT OUTER JOIN
                    T_Category AS Parent ON I.ParentId = Parent.Id
                 END
                END
             ");

            //66页，添加未分类类别
            var category = context.Categories.SingleOrDefault(m => m.Id == 10000);
            if (category == null)
            {
                context.Categories.Add(new Category()
                {
                    Title = "未分类",
                    Content = "未分类"
                });
            }
            else
            {
                category.Title = "未分类";
                category.Content = "未分类";
                category.ParentId = null;
            }
            context.SaveChanges();


            //67页加入的内容,Roles是Identity自动创建的类
            context.Roles.AddOrUpdate(p => p.Name,
               new ApplicationRole() { Name = "系统管理员" },
               new ApplicationRole() { Name = "编辑" },
               new ApplicationRole() { Name = "注册用户" }
           );

            //67页加入的内容,UserManager是Identity库提供的类
            var userManager = new UserManager<ApplicationUser, int>(
                new ApplicationUserStore(context)
                );
            var name = "admin";
            var user = userManager.FindByName(name);
            if (user != null) return;
            user = new ApplicationUser()
            {
                UserName = name,
                Email = "admin@amdin.com",
                IsApprove = true,
                Created = DateTime.Now
            };
            userManager.Create(user, "abcd1234");
            userManager.SetLockoutEnabled(user.Id, false);
            userManager.AddToRole(user.Id, "系统管理员");
        }
    }
}
