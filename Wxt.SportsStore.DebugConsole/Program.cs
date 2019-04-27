namespace Wxt.SportsStore.DebugConsole
{
    using System;
    using Wxt.SportsStore.Domain.Concrete;
    using Wxt.SportsStore.Domain.Entities;

    class Program
    {
        static void GenerateTestData()
        {
            EFDbContext context = new EFDbContext();

            context.Products.RemoveRange(context.Products);
            context.SaveChanges();
            var random = new Random();
            for (int i = 0; i < 4000; i++)
            {
                var cate = i / 103 + 1;
                Product product = new Product
                {
                    Name = $"Product{i + 1}",
                    Category = $"Category{cate}",
                    Description = $"Product{i + 1} in Category{cate}",
                    Price = (decimal)random.NextDouble() * 1000m
                };
                context.Products.Add(product);
            }
            context.SaveChanges();
        }

        static void Main(string[] args)
        {
            GenerateTestData();
        }

    }
}
