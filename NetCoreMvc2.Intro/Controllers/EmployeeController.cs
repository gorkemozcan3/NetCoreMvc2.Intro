using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMvc2.Intro.Entities;
using NetCoreMvc2.Intro.Models;

namespace NetCoreMvc2.Intro.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            var employeeAddViewModel = new EmployeeAddViewModel
            {
                Employee = new Employee(),
                Cities = new List<SelectListItem>
                {
                    new SelectListItem{Text="İzmit",Value="41"},
                    new SelectListItem{Text="İstanbul",Value="34",Selected=true},
                    new SelectListItem{Text="İzmir",Value="35"}
                }
            };

            return View(employeeAddViewModel);
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            return View();
        }
    }
}