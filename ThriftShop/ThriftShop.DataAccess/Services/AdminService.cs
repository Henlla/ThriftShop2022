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
    public class AdminService : Repository<Admin>, IAdmin
    {
        public ApplicationDbContext db;
        public AdminService(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public Task<Admin> Update(Admin account)
        {
            throw new NotImplementedException();
            
        }
    }
}
