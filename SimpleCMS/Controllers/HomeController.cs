﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCMS.Controllers
{
    public class HomeController : Controller
    {
        //首先用于48页的GitHub测试,75页用于创建首页
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}