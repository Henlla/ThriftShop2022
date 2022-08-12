using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.IRepository;

namespace ThriftShop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProduct Product { get; }
        ICategory Category { get; }
        IAdmin Admin { get; }
        IUserInfo UserInfo { get; }
        IUserAccount UserAccount { get; }
        IShoppingCart ShoppingCart { get; }
        ICoupon Coupon { get; }
        IFeedback Feedback { get; }
        IOrder Order { get; }
        IProductImage ProductImage { get; }
        IStock Stock { get; }
        public IOrderDetails orderDetails { get; }

        void Save();
    }
}
