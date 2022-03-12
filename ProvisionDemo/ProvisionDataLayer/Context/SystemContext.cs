using Microsoft.EntityFrameworkCore;
using ProvisionDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisionDataLayer.Context
{
    public class SystemContext : DbContext
    {
        public SystemContext()
        {
        }

        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"), b => b.MigrationsAssembly("ProvisionDataLayer"));
        }

        public DbSet<CurrencyCode> CurrencyCodes{ get; set; }
        public DbSet<TcmbData>TcmbDatas { get; set; }
    }
}
