using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Services;

namespace BulkyBook.DataAccess.Repository.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private  ApplicationDbContext _db;
        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductService(db);
            UserInfo = new UserInfoService(db);
            Admin = new AdminService(db);
        }
        public IProduct Product { get; private set; }
        public IUserInfo UserInfo { get; private set; }
        public IAdmin Admin { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
