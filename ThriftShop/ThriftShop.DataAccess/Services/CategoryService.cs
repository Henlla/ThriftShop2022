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
    public class CategoryService : Repository<Category>, ICategory
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Category> Update(Category obj)
        {
            var objFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.CategoryId == obj.CategoryId);
            if(objFromDb != null)
            {
                objFromDb.CategoryName = obj.CategoryName;
            }
            return obj;
        }


    }
}
