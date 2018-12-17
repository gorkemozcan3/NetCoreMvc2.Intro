using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NetCoreMvc2.Intro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvc2.Intro.ViewComponents
{
    public class StudentListViewComponent:ViewComponent
    {
        SchoolContext _context;
        public StudentListViewComponent(SchoolContext context)
        {
            _context = context;
        }

        public ViewViewComponentResult Invoke(string filter)
        {
            filter = HttpContext.Request.Query["filter"]; // query string ile parametre geçme
            return View(new StudentListViewModel {
                Students = _context.Students.Where(s=>s.FirstName.ToLower().Contains(filter)).ToList()
            });
        }
    }
}
