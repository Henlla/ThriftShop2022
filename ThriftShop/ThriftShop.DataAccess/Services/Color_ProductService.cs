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
    public class Color_ProductService : Repository<Color_Product>, IColor_Product
    {
        private readonly ApplicationDbContext _db;
        public Color_ProductService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Color_Product> Update(Color_Product obj)
        {
            var objFromDb = await _db.Color_Products.FirstOrDefaultAsync(u => u.ColorId == obj.ColorId && u.ProductId == obj.ProductId);
            if(objFromDb != null)
            {
                objFromDb.ColorId = obj.ColorId;
                objFromDb.ProductId = obj.ProductId;
            }
            return obj;
        }


    }
}
