using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAngular.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("[action]")]
        public List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id=1,FirstName="gorkem",LastName="ozcan"},
                new Customer{Id=2,FirstName="faruj",LastName="ozcan"},
                new Customer{Id=3,FirstName="aynur",LastName="ozcan"},
            };
        }

    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}