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
    public class ColorService : Repository<Color>, IColor
    {
        private readonly ApplicationDbContext _db;
        public ColorService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Color> Update(Color obj)
        {
            var objFromDb = await _db.Colors.FirstOrDefaultAsync(u => u.ColorId == obj.ColorId);
            if(objFromDb != null)
            {
                objFromDb.ColorType = obj.ColorType;
            }
            return obj;
        }


    }
}
