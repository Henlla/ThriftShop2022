using Microsoft.EntityFrameworkCore;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


    }
}