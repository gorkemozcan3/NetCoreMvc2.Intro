using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMvc2.Intro.Filters;

namespace NetCoreMvc2.Intro.Controllers
{
    public class FilterController : Controller
    {
        [CustomFilter] // Filters altındaki kodlar aşağıdaki action için çalışır
        public IActionResult Index()
        {
            return View();
        }
    }
}