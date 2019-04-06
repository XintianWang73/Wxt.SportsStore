using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wxt.SportsStore.Domain.Entities;

namespace Wxt.SportsStore.Domain.Concrete
{
    class EFDbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
