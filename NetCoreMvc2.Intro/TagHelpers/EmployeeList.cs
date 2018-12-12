using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreMvc2.Intro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
    Custom Tag helper oluşturuluyor
***/

namespace NetCoreMvc2.Intro.TagHelpers
{
    [HtmlTargetElement("employee-list")]
    public class EmployeeList:TagHelper // Tag Helper dan 
    {
        private List<Employee> _employees;
        public EmployeeList()
        {
            _employees = new List<Employee> {
                new Employee{Id=1,FirstName="Gorkem",LastName="Ozcan",CityId=34},
                new Employee{Id=2,FirstName="Faruk",LastName="Ozcan",CityId=41},
                new Employee{Id=3,FirstName="Aynur",LastName="Ozcan",CityId=41}
            };
        }

        // Custom attribute ekleme işlemi..
        private const string ListCountAttributeName = "count"; 

        [HtmlAttributeName(ListCountAttributeName)] // count yazarak attribute belirtebiliyoruz. ListCount a tanımladığımız için otomatik atama yapıcak
        public int ListCount { get; set; }
        // Custom attribute ekleme işlemi
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // bir div in içine yazar
            var query = _employees.Take(ListCount);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in query)
            {
                stringBuilder.AppendFormat("<h2><a href='/employee/detail/{0}'>{1}</a></h2>", item.Id, item.FirstName);
            }

            output.Content.SetHtmlContent(stringBuilder.ToString());

            base.Process(context, output);
        }
    }
}
