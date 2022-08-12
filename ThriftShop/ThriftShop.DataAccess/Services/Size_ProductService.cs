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
    public class Size_ProductService : Repository<Size_Product>, ISize_Product
    {
       
    private readonly ApplicationDbContext _db;
        public Size_ProductService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Size_Product> Update(Size_Product obj)
        {
            var objFromDb = await _db.Size_Products.FirstOrDefaultAsync(u => u.SizeId == obj.SizeId && u.ProductId == obj.ProductId);
            if(objFromDb != null)
            {
                objFromDb.ProductId = obj.ProductId;
                objFromDb.SizeId = obj.SizeId;
            }
            return obj;
        }


    }
}
