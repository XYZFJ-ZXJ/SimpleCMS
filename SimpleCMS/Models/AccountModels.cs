using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleCMS.LocalResources;

namespace SimpleCMS.Models
{
    //129底创建的文件，与登录有关的实体，被AccountController有关
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Message))]
        [Display(Name = "UserUserName", ResourceType = typeof(Message))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Message))]
        [Display(Name = "UserPassword", ResourceType = typeof(Message))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Message))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Message))]
        [MaxLength(6, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Message))]
        [Display(Name = "VerifyCode", ResourceType = typeof(Message))]
        public string VerifyCode { get; set; }

        public string RememberMe { get; set; }
    }

    //140页底部常创建的类，是与用户密码重新设置有关的实体
    public class PasswordResetModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Message))]
        [Display(Name = "OldPassword", ResourceType = typeof(Message))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Message))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Message))]
        [RegularExpression(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z\W]{6,}$", ErrorMessage = "Password", ErrorMessageResourceType = typeof(Message))]
        [Display(Name = "NewPassword", ResourceType = typeof(Message))]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Message))]
        [Compare("NewPassword", ErrorMessageResourceName = "Compare", ErrorMessageResourceType = typeof(Message))]
        public string ConfirmPassword { get; set; }
    }

}