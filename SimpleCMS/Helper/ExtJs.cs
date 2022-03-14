using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using SimpleCMS.LocalResources;
using System.Linq.Dynamic;

//98页创建的类,用于在服务器端统一输出接口和实体状态ModelState对象内的信息保存到JObject对象（是Json.NET内提供的类）
//扩展代理，前端统一输入/输入接口用的是Sencha/app/ux/Format.js文件（JSON接口），接口定义一致，前后端就可以数据交流了
namespace SimpleCMS.Helper
{
    public class ExtJs
    {
        public static JObject WriterJObject(
            bool success,
            JObject errors = null,
            int? total = null,
            string msg = null,
            object data = null
        )
        {
            var jo = new JObject()
            {
                { "success", success }
            };
            if (errors != null)
            {
                jo.Add(new JProperty("errors", errors));
            }
            if (total != null)
            {
                jo.Add(new JProperty("total", total));
            }
            if (msg != null)
            {
                jo.Add(new JProperty("msg", msg));
            }
            if (data != null)
            {
                jo.Add(new JProperty("data", data));
            }
            return jo;
        }

        //130页创建的方法，用于C#状态对象ModelState对象状态信息的展示，130页用来保存login提交信息的验证状态.
        //JObject使用命名空间using Newtonsoft.Json.Linq;，,是Json.NET的库文件(49),用于处理各种JSon数据,以便与客户端进行交互.
        //Where(),Select()方法是
        public static JObject ModelStateToJObject(ModelStateDictionary modelState)
        {
            var errors = new JObject();
            var q = modelState.Where(m => !modelState.IsValidField(m.Key)).Select(m => m.Key);
            foreach (var c in q)
            {
                errors.Add(new JProperty(c, string.Join("<br/>",
                    modelState[c].Errors.Select(m => m.ErrorMessage))));
            }
            return errors;
        }

    }
}

