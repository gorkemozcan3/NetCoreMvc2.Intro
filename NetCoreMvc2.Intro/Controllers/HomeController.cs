using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMvc2.Intro.Entities;
using NetCoreMvc2.Intro.Filters;
using NetCoreMvc2.Intro.Models;

namespace NetCoreMvc2.Intro.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello MVC Core";
        }

        [HandleException(ViewName ="Error",ExceptionType = typeof(DivideByZeroException))] // Hata alınma durumunda Filters altındaki class ı cağırıyor
        public ViewResult Index2()
        {
            throw new DivideByZeroException();
            //throw new Exception("hata...");
            return View();
        }

        public ViewResult Index3()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };

            List<string> cities = new List<string> { "istanbul", "Kocaeli" };

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                Cities = cities
            };

            return View(model);
        }

        // Status Code döndürme
        public StatusCodeResult Index4() // Genel  ifadeyle return ttype ı IActionResult da olabilir
        {
            return Ok(); // veya StatusCode(200)
        }

        public StatusCodeResult Index5() // Genel  ifadeyle return ttype ı IActionResult da olabilir
        {
            return BadRequest(); // veya StatusCode(400)
        }

        // Redirect işlemleri.. Bir işlem tamamlandığında yeni sayfaya yönlendirme vs.
        public RedirectResult Index6()
        {
            return Redirect("/Home/Index2"); // 1. yöntem
        }

        public IActionResult Index7()
        {
            return RedirectToAction("Index3", "Home"); // 2. yöntem (daha çok kullanılıyor)
        }

        public IActionResult Index8()
        {
            return RedirectToRoute("default"); // 3. yöntem - Startup.cs de tanımladığımız route ın name kısmı parametre geçiliyor
            // Şu an default a göre calıştığından sonsuz döngüye giriyor, sürekli kendini redirect ediyor !!!
            // Çok kullanılan bir yöntem değil
        }

        // Json result döndürme
        public JsonResult Index9()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };

            return Json(employees);
        }

        // Razor işlemleri için datalar gönderiliyor
        public IActionResult RazorDemo()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };

            List<string> cities = new List<string> { "istanbul", "Kocaeli" };

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                Cities = cities
            };

            return View(model);
        }

        // Query string ile Model Binding işlemleri (örnek olarak; alınan parametreye göre arama işlemi)
        // home/index10?key=a
        public JsonResult Index10(string key)
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };

            if (String.IsNullOrEmpty(key))
            {
                return Json(employees);
            }

            var result = employees.Where(e => e.FirstName.ToLower().Contains(key));

            return Json(result);
        }

        // Form Datası ile Model Binding işlemleri (örnek olarak; alınan parametreye göre arama işlemi)
        // View üzerinden arama yapılacak parametre textbox ile alınıyor
        public ViewResult EmployeeForm()
        {
            return View();
        }

        // Route Datası ile Model Binding işlemleri (örnek olarak; alınan parametreye göre arama işlemi)
        // home/RouteData/15 ==> sona yazılan değer parametre oluyor, query string gibi...
        public string RouteDataa(int id)
        {
            return id.ToString();
        }
    }
}