using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetCoreMvc2.Intro.Pages.Customer
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }

        // OnGet metodu view cagrıldığında çalısır
        public void OnGet()
        {
            Message = "Bugün Tarih " + DateTime.Now;
        }
    }
}