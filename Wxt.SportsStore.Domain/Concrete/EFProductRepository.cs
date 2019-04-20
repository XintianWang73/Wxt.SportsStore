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
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products => _context.Products;

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = _context.Products.Find(productId);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
