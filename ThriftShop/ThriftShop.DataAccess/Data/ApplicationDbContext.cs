using Microsoft.EntityFrameworkCore;

namespace ThriftShop.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        //public DbSet<> MyProperty { get; set; }
    }
}