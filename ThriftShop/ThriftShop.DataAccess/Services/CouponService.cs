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
    public class CouponService :Repository<Coupon>,ICoupon
    {
        private readonly ApplicationDbContext _db;
        public CouponService(ApplicationDbContext _db):base(_db)
        {
            this._db = _db;
        }

        public async Task<Coupon> Update(Coupon obj)
        {
            _db.Coupons.Update(obj);
            return obj;
        }
    }
}
