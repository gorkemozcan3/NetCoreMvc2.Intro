using System.Collections.Generic;
using NetCoreMvc2.Intro.Entities;

namespace NetCoreMvc2.Intro.Models
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<string> Cities { get; set; }
    }
}