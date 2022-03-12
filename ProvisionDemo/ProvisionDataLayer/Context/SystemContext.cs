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
        public SystemContext(DbContextOptions<SystemContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemContext).Assembly);
        }

        public DbSet<CurrencyCode> CurrencyCodes{ get; set; }
        public DbSet<TcmbData>TcmbDatas { get; set; }
    }
}
