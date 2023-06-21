using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.IRepository.GenericInterface;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services
{
    public class UserInfoService : Repository<UserInfo>, IUserInfo
    {
        public ApplicationDbContext db;
        public UserInfoService(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public async Task<UserInfo> Update(UserInfo user)
        {
            var model = await db.UserInfos.SingleOrDefaultAsync(u => u.UserId.Equals(user.UserId));
            model.Name = user.Name;
            model.Phone = user.Phone;
            model.PostalCode = user.PostalCode;
            model.State = user.State;
            model.Address = user.Address;
            model.Email = user.Email;
            model.City = user.City;
            model.Gender = user.Gender;
            return model;
        }
    }
}
