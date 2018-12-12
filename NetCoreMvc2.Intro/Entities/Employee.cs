using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvc2.Intro.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        // Data annotation -- görüntüyü düzenliyor...
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
    }
}
