using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using SimpleCMS.LocalResources;
using System.Linq.Dynamic;

//98页创建的类,用于在服务器端统一输出接口
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

    }
}

