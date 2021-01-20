using ejner_hessel_case.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejner_hessel_case.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)   
            : base(options)
        {
        }
    }
}
