using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//自己加入的内容
using SimpleCMS.Helper;

namespace SimpleCMS.Controllers
{
    //125页创建的类
    public class VerifyCodeController : Controller
    {
        // GET: VerifyCode
        public FileContentResult Index()
        {
            var v = new VerifyCode();
            var code = v.CreateVerifyCode();                //取随机码
            Session["VerifyCode"] = code;
            v.Padding = 10;
            var bytes = v.CreateImage(code);
            return File(bytes, @"image/jpeg");
        }
    }
}