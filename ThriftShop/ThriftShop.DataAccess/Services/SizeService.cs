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
    public class SizeService : Repository<Size>, ISize
    {
        private readonly ApplicationDbContext _db;
        public SizeService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Size> Update(Size obj)
        {
            var objFromDb = await _db.Sizes.FirstOrDefaultAsync(u => u.SizeId == obj.SizeId);
            if(objFromDb != null)
            {
                objFromDb.SizeType = obj.SizeType;
            }
            return obj;
        }


    }
}
