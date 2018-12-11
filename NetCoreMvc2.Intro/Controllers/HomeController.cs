using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMvc2.Intro.Entities;

namespace NetCoreMvc2.Intro.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello MVC Core";
        }

        public ViewResult Index2()
        {
            return View();
        }

        public ViewResult Index3()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };
            return View(employees);
        }
    }
}