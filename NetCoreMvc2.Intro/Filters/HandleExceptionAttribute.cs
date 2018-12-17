using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvc2.Intro.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        // Attribute için parametre tanımları
        public string ViewName { get; set; }
        public Type ExceptionType { get; set; }

        public override void OnException(ExceptionContext context)
        {
            if (ExceptionType != null)
            {
                if (context.Exception.GetType() == ExceptionType)
                {
                    var result = new ViewResult { ViewName = this.ViewName }; // gelen viewe göre gidecek
                    var modelDataProvider = new EmptyModelMetadataProvider();
                    // view e data olarak exception gönderilyor.
                    result.ViewData = new ViewDataDictionary(modelDataProvider, context.ModelState);
                    result.ViewData.Add("HandleException", context.Exception);
                    context.Result = result;
                    context.ExceptionHandled = true;
                }
            }

        }
    }
}
