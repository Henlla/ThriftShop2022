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
    public class OrderService:Repository<Order>,IOrder
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext _db):base(_db)
        {
            this._db = _db;
        }

        public Task<Order> Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
