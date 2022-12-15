using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rimot.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimot.Server.Data
{
    public class TestingDbContext : AppDb
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("Rimot");
            base.OnConfiguring(options);
        }
    }
}
