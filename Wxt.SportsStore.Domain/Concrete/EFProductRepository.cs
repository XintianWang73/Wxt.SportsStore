namespace Wxt.SportsStore.Domain.Concrete
{
    using System.Collections.Generic;
    using Wxt.SportsStore.Domain.Abstract;
    using Wxt.SportsStore.Domain.Entities;

    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext _context;

        public EFProductRepository(EFDbContext context)
        {
            _context = context;
        }

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
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            _context.SaveChanges();
        }
    }
}
