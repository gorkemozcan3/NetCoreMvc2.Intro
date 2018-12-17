using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreMvc2.Intro.Entities;
using NetCoreMvc2.Intro.Models;

namespace NetCoreMvc2.Intro.Pages.Studentp
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public List<Student> Students { get; set; }

        // OnGet metodu view cagrıldığında çalısır ==> Studentp/Index?search=x
        public void OnGet(string search) // parametreli kullanım için (örnek olarak; arama yapılacak)
        {
            Students = String.IsNullOrEmpty(search)
            ? _context.Students.ToList()
            : _context.Students.Where(s=>s.FirstName.ToLower().Contains(search)).ToList();
        }

        [BindProperty] // Viewde yer alan ve Student ile başlayan propertyleri aşağıdaki nesneye bind eder
        public Student Student { get; set; }
        public IActionResult OnPost()
        {
            _context.Students.Add(Student);
            _context.SaveChanges(); // veritabanında commit benzeri işlem yapıyo

            return RedirectToPage("/Studentp/Index");
        }
    }
}