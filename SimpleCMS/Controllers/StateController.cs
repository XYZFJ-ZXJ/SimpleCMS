using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    [Authorize]
    public class StateController : BaseController
    {
        //本文件是88页添加的
        ////本文件是视图状态管理的后端的其中一个部分,还要与后端的model下的UserProfile.cs实体文件，
        //前端的Sencha/app/utilState.js结合使用才能达到总的视图状态管理的功能
        // GET: State
        public void Save(string key,string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
            var userId = User.Identity.GetUserId<int>();
            var q = DbContext.UserProfiles.SingleOrDefault(m => m.Keyword.Equals(key) && m.UserProfileType == (byte)UserProfileType.State && m.UserId == userId);
            if (q == null)
            {
                DbContext.UserProfiles.Add(new UserProfile()
                {
                    Keyword = key,
                    Value = value,
                    UserProfileType = (byte)UserProfileType.State,
                    UserId = userId
                });
            }
            else
            {
                q.Value = value;
            }
            DbContext.SaveChanges();
        }

        //Restore方法是90A处添加的代码
        public JObject Restore()
        {
            var userId = User.Identity.GetUserId<int>();
            var q = DbContext.UserProfiles.Where(m => m.UserProfileType == (byte)UserProfileType.State && m.UserId == userId).Select(m => new
            {
                Key = m.Keyword,
                Value = m.Value
            });
            return new JObject()
            {
                { "success", true },
                { "data", JArray.FromObject(q) }
            };
        }
    }
}