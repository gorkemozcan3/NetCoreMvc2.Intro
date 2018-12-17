using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreMvc2.Intro.Controllers
{
    public class CommonController : Controller
    {
        [Route("/error")] // startup.cs içinde verilen exception handlerda aşağıdaki action çalışacak
        public IActionResult Index()
        {
            return View();
        }
    }
}