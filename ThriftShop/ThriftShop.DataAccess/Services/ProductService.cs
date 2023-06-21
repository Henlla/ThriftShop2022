using Microsoft.EntityFrameworkCore;
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

        public void HandlerJsonImageList(string JsonImageList)
        {
           
        }

        public async Task<Product> Update(Product product)
        {
            var _product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
            if (_product != null)
            {
                _product.Title = product.Title;
                _product.Description = product.Description;
                _product.CategoryId = product.CategoryId;
                _product.Price = product.Price;
                _product.SalePercent = product.SalePercent;
                _product.SexType = product.SexType;
                return product;
            }
            else
            {
                return null;
            }
    }
    }
}
