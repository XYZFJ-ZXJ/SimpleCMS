using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    //71页底和72页添加的内容
    public class BaseController : Controller
    {
        //C#变量，用在下面的C#属性里面
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext _dbContext;

        public BaseController()
        {
        }

        public BaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, ApplicationDbContext dbContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            DbContext = dbContext;
        }

        //C#属性，在里面使用了上面定义的变量
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationDbContext DbContext
        {
            get
            {
                return _dbContext ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _dbContext = value;
            }
        }

        //133页底部加入的属性，被AccountController等类使用，代表当前用户
        public ApplicationUser CurrentUser
        {
            get
            {
                var currentUser = UserManager.FindByName(User.Identity.Name);
                return currentUser;
            }
        }

    }
}