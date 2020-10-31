using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StravaTEC.Database
{
    public class StraviaContext : DbContext
    {

        public StraviaContext(DbContextOptions<StraviaContext> options) : base(options)
        {

        }

    }
}
