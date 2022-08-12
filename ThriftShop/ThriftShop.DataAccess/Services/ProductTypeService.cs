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
    public class ProductTypeService : Repository<ProductType>, IProductType
    {
        private readonly ApplicationDbContext _db;
        public ProductTypeService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

       
        public async Task<ProductType> Update(ProductType obj)
        {
            var objFromDb = await _db.ProductTypes.FirstOrDefaultAsync(u => u.ProductTypeId == obj.ProductTypeId);
            if(objFromDb != null)
            {
                objFromDb.Type = obj.Type;
            }
            return obj;
        }


    }
}
