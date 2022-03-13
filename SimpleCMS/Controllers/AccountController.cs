using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using SimpleCMS.Helper;
using SimpleCMS.LocalResources;
//130上部创建的类，需要 使用AccountModel.cs的LoginModel类和PasswordResetModel类
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //129页底部创建的类，使用Identity实现登录
        // GET: Account,Login方法使用async关键字的原因是方法体内使用了awit关键字和异步方法.
        //LoginModel实体类是位于AccountModel.cs的实体类
        [AllowAnonymous]
        [HttpPost]
        public async Task<JObject> Login(LoginModel model)
        {
            //131页，验证提交数据
            if (!ModelState.IsValid)
            {
                return ExtJs.WriterJObject(false, errors: ExtJs.ModelStateToJObject(ModelState));
            }
            //验证输入的验证码是否正确
            var verifyCode = (string)Session["VerifyCode"] ?? "";
            if (string.IsNullOrEmpty(model.VerifyCode) || !string.Equals(verifyCode, model.VerifyCode, StringComparison.CurrentCultureIgnoreCase))
            {
                return ExtJs.WriterJObject(false, errors: new JObject() { { "VerifyCode", Message.VerifyCode } });
            }
            //验证用户是否存在或者是否已被禁止登陆，用到了AccountModel.cs的LoginModel实体类
            //UserManager是BaseController.cs内的属性对象<在BaseController已经使用了Owin注入了，UserManager最终是68页IdentityConfig.cs中的>
            //model为版本类Login方法的参数，来自于LoginModel实体类，即Models/AccountModel.cs类文件.FindByNameAsync是UserManager的方法，定义于Identity库<功能类似于spring security框架>内.
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return ExtJs.WriterJObject(false, errors: new JObject()
                {
                    {"UserName", Message.SignInFailure} ,
                    {"Password", Message.SignInFailure}
                });
            }
            if (!user.IsApprove)
            {
                return ExtJs.WriterJObject(false, errors: new JObject()
                {
                    { "UserName", Message.IsApprove}
                });
            }

            //用户已存在，验证输入的密码是否正确，同样SignInManager最终是68页IdentityConfig.cs中的ApplicationSignInManager类的对象
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe == "on", shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    user.LastLogin = DateTime.Now;
                    await UserManager.UpdateAsync(user);
                    return ExtJs.WriterJObject(true);
                case SignInStatus.LockedOut:
                    return ExtJs.WriterJObject(false, errors: new JObject()
                    {
                        new JProperty("UserName", Message.LockedOut)
                    });
                case SignInStatus.RequiresVerification:
                case SignInStatus.Failure:
                default:
                    return ExtJs.WriterJObject(false, errors: new JObject()
                    {
                        new JProperty("UserName", Message.SignInFailure),
                        new JProperty("Password", Message.SignInFailure)
                    });
            }

        }

        //133页添加，用于展示用户的信息,CurrentUser属性定义于BaseController类内
        [AllowAnonymous]
        public async Task<JObject> UserInfo()
        {
            if (!User.Identity.IsAuthenticated) return ExtJs.WriterJObject(false);
            if (CurrentUser == null) return ExtJs.WriterJObject(false);
            var roles = await UserManager.GetRolesAsync(CurrentUser.Id);
            return ExtJs.WriterJObject(true, data: new JObject()
            {
                {
                    "UserInfo", new JObject()
                    {
                        {"UserName", CurrentUser.UserName},
                        {"Roles", JArray.FromObject(roles)}
                    }
                },
                {"Menu", GetMenu(roles.Contains("系统管理员"))}  //GetMenu为下一个定义方法
            });
        }

        //134页定义的方法，被上一个方法调用
        private JArray GetMenu(bool isAdmin)
        {
            //这里可以从数据库获取导航菜单返回
            var menus = new JArray()
            {
                new JObject(){
                    { "text" , "文章管理"},
                    { "iconCls" , "x-fa fa-file-text-o"},
                    { "rowCls" , "nav-tree-badge"},
                    { "viewType", "articleView" },
                    { "routeId", "articleview" },
                    { "leaf", true }
                },
                new JObject()
                {
                    { "text" , "媒体管理"},
                    { "iconCls" , "x-fa fa-file-image-o"},
                    { "rowCls" , "nav-tree-badge"},
                    { "viewType", "mediaView" },
                    { "routeId", "mediaView" },
                    { "leaf", true }
                }
            };
            if (isAdmin)
            {
                menus.Add(new JObject()
                {
                    { "text" , "用户管理"},
                    { "iconCls" , "x-fa fa-user"},
                    { "rowCls" , "nav-tree-badge"},
                    { "viewType", "userView" },
                    { "routeId", "userView" },
                    { "leaf", true }
                });
            }
            return menus;
        }

        //138页，登出操作的后端部分
        public JObject LogOut()
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return ExtJs.WriterJObject(true);
        }

        //141页顶部创建的代码，用于修改用户密码的后端部分的Controller,另一部分是位于AccountModels.cs内的PasswordResetModel
        public async Task<JObject> PasswordReset(PasswordResetModel model)
        {
            //验证新密码是否和旧密码相同
            if (model.Password.Equals(model.NewPassword))
            {
                ModelState.AddModelError("Password", Message.OldPasswordEqualNew);
            }
            if (!ModelState.IsValid)
            {
                return ExtJs.WriterJObject(false, errors: ExtJs.ModelStateToJObject(ModelState));
            }
            //实现用户密码修改
            var userId = User.Identity.GetUserId<int>();
            var result = await UserManager.ChangePasswordAsync(userId, model.Password, model.NewPassword);
            if (result.Succeeded)
            {
                SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return ExtJs.WriterJObject(true);
            }
            else
            {
                return ExtJs.WriterJObject(false, errors: new JObject()
                {
                    { "Password", string.Join("<br/>", result.Errors)}
                });
            }
        }

    }
}
