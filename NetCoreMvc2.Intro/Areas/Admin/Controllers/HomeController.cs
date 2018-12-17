﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreMvc2.Intro.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")] // area için routing
        public IActionResult Index()
        {
            return View();
        }
    }
}