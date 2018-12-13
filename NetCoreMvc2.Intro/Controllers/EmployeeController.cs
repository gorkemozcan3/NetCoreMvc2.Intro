using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMvc2.Intro.Entities;
using NetCoreMvc2.Intro.Models;
using NetCoreMvc2.Intro.Services;

namespace NetCoreMvc2.Intro.Controllers
{
    public class EmployeeController : Controller
    {
        // Dependency Injection için calculatur interfacei constructor içinde parametre olarak alınıp private değişkene atanıyor.
        // Dependency Injection implemantasyonu sağlandı
        // Startup.cs içinde ICalculator ı hangi class ı çağıracağı atanıyor
        private ICalculator _calculator;
        public EmployeeController(ICalculator calculator)
        {
            _calculator = calculator;
        }

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

        public string Calculate()
        {
            return _calculator.Calculate(100).ToString();
        }
    }
}