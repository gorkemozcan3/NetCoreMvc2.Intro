using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreMvc2.Intro.Controllers
{
    [Route("admin")] // main route
    public class AdminController : Controller
    {
        [Route("")] // boş olduğunda default olarak save çalışır
        [Route("save")] // save yazıldığında da çalış
        [Route("~/save")] // önünde admin olmasa da çalış
        public string Save()
        {
            return "saved";
        }

        [Route("delete/{id?}")] // parametreli kullanım
        public string Delete(int id=0)
        {
            return "deleted " + id.ToString();
        }

        [Route("update")]
        public string Update()
        {
            return "updated";
        }
    }
}