using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository.GenericInterface;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services.Generic_Imp
{
    public class ProductService : IProduct
    {
        private ApplicationDbContext _db;
        public ProductService(ApplicationDbContext _db)
        {
            this._db = _db;
        }

        public Task<Product> Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetFirstOrDefault(Expression<Func<Product, bool>> filter, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
