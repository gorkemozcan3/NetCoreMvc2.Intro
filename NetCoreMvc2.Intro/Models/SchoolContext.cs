using Microsoft.EntityFrameworkCore;
using NetCoreMvc2.Intro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvc2.Intro.Models
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
