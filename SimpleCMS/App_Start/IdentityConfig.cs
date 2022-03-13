using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
//Helper类的作用见71页顶部A处，使用到了Helper/AppSetting.cs辅助类
using SimpleCMS.Helper;
using SimpleCMS.Models;

namespace SimpleCMS
{
    //本文件相当于是Identity的配置文件，相当于Spring Security的spring-security.xml文件.最终需要Web.Config内的<appSettings>配置项
    //本文件通过在第八章登录与权限控制章节在C#类中使用特性<在符号"[]"中的代码>的方式实现Identity库和Owin库的使用
    //本文件定义的这些带Manager的管理类要被70页的Startup.Auth.cs注册到Owin。
    public class EmailService : IIdentityMessageService
    {
        //EmailService和SmaService用于做双因子验证的，本仰慕不需要，不用做任何修改
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    //本类用来做管理用户的配置，提供各种验证策略和锁定策略
    //UserManager见67页顶部，IUserStore都是Identity库提供的类,context.Get()是Owin上下文的方法
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser,int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
        }

        //69页对该方法进行了修改,使用了Owin上下文,ApplicationUserStore,ApplicationUser是57页重新定义的实体.
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            //统一实体和Store,相当于将实体集类的数据注入到实体模型类中,创建manager对象
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames，
            //通过ApplicationUserManager的UserValidator属性来配置用户名的验证策略.
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                //71页所作出的一个修改
                AllowOnlyAlphanumericUserNames = AppSettings.GetSettingAsBool("AllowOnlyAlphanumericUserNames") ?? true,
                RequireUniqueEmail = AppSettings.GetSettingAsBool("RequireUniqueEmail") ?? true
            };

            // Configure validation logic for passwords
            //通过ApplicationUserManager的PasswordValidator属性来配置密码的验证策略.
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = AppSettings.GetSettingAsInteger("RequiredLength") ?? 6,
                RequireNonLetterOrDigit = AppSettings.GetSettingAsBool("RequireNonLetterOrDigit") ?? false,
                RequireDigit = AppSettings.GetSettingAsBool("RequireDigit") ?? true,
                RequireLowercase = AppSettings.GetSettingAsBool("RequireLowercase") ?? true,
                RequireUppercase = AppSettings.GetSettingAsBool("RequireUppercase") ?? false,
            };

            // Configure user lockout defaults
            //通过ApplicationUserManager进行锁定策略的配置(锁定就是用户名密码输入错误达到这里配置的次数后就会一定时间内不能登录).
            manager.UserLockoutEnabledByDefault = AppSettings.GetSettingAsBool("UserLockoutEnabledByDefault") ?? true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(AppSettings.GetSettingAsInteger("DefaultAccountLockoutTimeSpan") ?? 60);
            manager.MaxFailedAccessAttemptsBeforeLockout = AppSettings.GetSettingAsInteger("MaxFailedAccessAttemptsBeforeLockout") ?? 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            //双因子验证的配置，本项目不需要使用双因子验证，本类后面的代码不用修改
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    //69页的类，本类用于做管理用户登录的配置
    // Configure the application sign-in manager which is used in this application.
    //ApplicationUser是57页定义的用户实体类,SignInManager是Owin提供的
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        //ApplicationUserManage是本文件的上一个类,IAuthenticationManager是Owin.Security提供的类
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        //创建一个静态的Creat方法，以便在Owin中注册，这是使用Owin的规范(55页底)
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    //69页添加，实现角色管理的配置
    //RoleManager是Identity提供的类，ApplicationRole是56页底部定义的类，ApplicationRole是57页底部定义的类
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        //统一实体和Store,相当于将实体集类的数据注入到实体模型类中
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
            : base(roleStore)
        {
        }

        //创建一个静态的Creat方法，以便在Owin中注册，这是使用Owin的规范(55页底)
        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
        }
    }

}
