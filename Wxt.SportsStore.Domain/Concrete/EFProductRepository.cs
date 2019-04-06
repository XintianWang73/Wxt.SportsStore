using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wxt.SportsStore.Domain.Abstract;
using Wxt.SportsStore.Domain.Entities;

namespace Wxt.SportsStore.Domain.Concrete
{
    public class EFProductRepository: IProductsRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
