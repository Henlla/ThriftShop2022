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
    internal class UserService : Repository<UserAccount>, IUserAccount
    {
        private ApplicationDbContext db;
        public UserService(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public Task<UserAccount> Update(UserAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
