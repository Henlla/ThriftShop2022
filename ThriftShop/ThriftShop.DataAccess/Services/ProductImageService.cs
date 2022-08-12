using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services
{
    public class ProductImageService : Repository<ProductImage>, IProductImage
    {
        private readonly ApplicationDbContext _db;
        public ProductImageService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<ProductImage> Update(ProductImage obj)
        {
            var objFromDb = await _db.ProductImages.FirstOrDefaultAsync(u => u.ProductId == obj.ProductId);
            if(objFromDb != null)
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
            return obj;
        }

       
    }
}
