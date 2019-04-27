namespace Wxt.SportsStore.Domain.Concrete
{
    using System.Data.Entity;
    using Wxt.SportsStore.Domain.Entities;

    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
    }
}
