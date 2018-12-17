using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NetCoreMvc2.Intro.ExtensionMethods;
using NetCoreMvc2.Intro.Entities;

namespace NetCoreMvc2.Intro.Controllers
{
    public class SessionDemoController : Controller
    {
        public string Index()
        {
            HttpContext.Session.SetInt32("age", 22);
            HttpContext.Session.SetString("name", "Gorkem");
            HttpContext.Session.SetObject("student", new Student { FirstName = "gorkem", LastName = "ozcan", Email = "gea@1e.cm" });
            return "session oluştu";
        }

        public string GetSession()
        {
            Student student = HttpContext.Session.GetObject<Student>("student");
            return "Sessiion name: " + HttpContext.Session.GetString("name") + " age: " + HttpContext.Session.GetInt32("age")
                + " student: " + student.Email + "," + student.FirstName + "," + student.LastName;
        }
    }
}