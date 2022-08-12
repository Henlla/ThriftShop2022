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
    public class OrderDetailsServices:Repository<OrderDetail>,IOrderDetails
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailsServices(ApplicationDbContext _db) : base(_db)
        {
            this._db = _db;
        }

        public Task<OrderDetail> Update(OrderDetail obj)
        {
            throw new NotImplementedException();
        }
    }
}
