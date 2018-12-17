using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvc2.Intro.Filters
{
    public class CustomFilter : Attribute, IActionFilter
    {
        // Attribute: attribute olarak kullanılmasını sağlıyor
        // IActionFilter: bir filter olduğunu ifade ediyor.
        // Action başlarken ve bittiğinde belirli fonksiyonların otomatik olarka çalışmasını sağlar
        // Loglama işlemlerinde yoğunlukla kullanılıyor
        public void OnActionExecuting(ActionExecutingContext context)
        {
            int i = 10;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            int i = 20;
        }
    }
}
