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
    public class ShoppingCartService : Repository<ShoppingCart>, IShoppingCart
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartService(ApplicationDbContext _db):base(_db)
        {
            this._db = _db;
        }

        public async Task<ShoppingCart> Update(ShoppingCart obj)
        {
            var model = await _db.ShoppingCarts.FindAsync(obj.Id);
            if (model != null) {
                model.Count = obj.Count;
                _db.ShoppingCarts.Update(model);
            }
            return model;
        }
    }
}
