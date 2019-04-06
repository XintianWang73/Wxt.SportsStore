using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wxt.SportsStore.Domain.Concrete;
using Wxt.SportsStore.Domain.Entities;

namespace Wxt.SportsStore.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new EFDbContext())
            {
                ctx.Products.RemoveRange(ctx.Products);
                ctx.SaveChanges();
                for (int i = 0; i < 100; i++)
                {
                    var product = new Product()
                    {
                        Name = "Products"+i,
                        Price = 1m,
                        Description = "aaa",
                        Category = "bbb"
                    };
                    ctx.Products.Add(product); 
                }

                ctx.SaveChanges();
            }
            //Console.ReadKey();
        }
    }
}
