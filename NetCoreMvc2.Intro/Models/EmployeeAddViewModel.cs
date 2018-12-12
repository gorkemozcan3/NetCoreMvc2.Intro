using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMvc2.Intro.Entities;

namespace NetCoreMvc2.Intro.Models
{
    public class EmployeeAddViewModel
    {
        public Employee Employee { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}