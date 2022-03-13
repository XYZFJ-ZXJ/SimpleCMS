using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SimpleCMS.Models
{
    //本文件内的类是Identity验证需要用到的用户和角色实体及其对应数据集，也是属于EF框架的内容()
    //56页,因为在ApplicationDbContext中将用户和角色的主键类型由字符串修改为了整形，因此创建该文件用于重新定义与Identity相关的实体类.
    //本文件在同一个命名空间下定义了多个互相独立的类
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserRole : IdentityUserRole<int> { }

    //角色实体类，被67页使用，被69页使用
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>
    {
        public string Description { get; set; }

        public ApplicationRole() : base() { }
        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

    }

    //57,用户实体类,被67页使用,被69页使用
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<int>
    {
        public async Task<ClaimsIdentity>
            GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public bool IsApprove { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastLogin { get; set; }
    }


    //以下两个类为用户实体和角色实体的数据集
    public class ApplicationUserStore :
       UserStore<ApplicationUser, ApplicationRole, int,
       ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUserStore<ApplicationUser, int>, IDisposable
    {
        public ApplicationUserStore()
            : this(new ApplicationDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore
        : RoleStore<ApplicationRole, int, ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole, int>,
        IRoleStore<ApplicationRole, int>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new ApplicationDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

}
