using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services
{
    public class ProductService : Repository<Product>, IProduct
    {
        private ApplicationDbContext db;
        public ProductService(ApplicationDbContext _db) :base(_db)
        {
            db = _db;
        }

        public Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
