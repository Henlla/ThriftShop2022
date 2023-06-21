using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ThriftShop.DataAccess.Services
{
    public class OrderService:Repository<Order>,IOrder
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext _db):base(_db)
        {
            this._db = _db;
        }

        public async Task<Order> Update(Order obj)
        {
            var model = await _db.Orders.FindAsync(obj.Id);
            if (model != null)
            {
                model.OrderStatus = obj.OrderStatus;
                _db.Orders.Update(model);
              

            }
           
                return model;
            
        }
    }
}
