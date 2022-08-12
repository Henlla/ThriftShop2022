using ThriftShop.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Services;

namespace ThriftShop.DataAccess.Repository.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private  ApplicationDbContext _db;
        
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductService(db);
            Category = new CategoryService(db);
            ShoppingCart = new ShoppingCartService(db);
            Admin = new AdminService(db);
            UserInfo = new UserInfoService(db);
            UserAccount = new UserService(db);
            Coupon = new CouponService(db);
            Feedback = new FeedbackService(db);
            Order = new OrderService(db);
            Stock = new StockService(db);
            Size = new SizeService(db);
            Color = new ColorService(db);
            ProductType = new ProductTypeService(db);
            ProductImage = new ProductImageService(db);
            orderDetails = new OrderDetailsServices(db);
        }

        public IProduct Product { get; private set; }
        public ICategory Category { get; private set; }
        public IAdmin Admin { get; private set; }
        public IUserInfo UserInfo { get; private set; }
        public IUserAccount UserAccount { get; private set; }
         public IShoppingCart ShoppingCart { get; private set; }
         public ICoupon Coupon { get; private set; }
         public IFeedback Feedback { get; private set; }
        public IOrder Order { get; private set; }
        public IStock Stock { get; private set; }
        public IColor Color { get; private set; }
        public ISize Size { get; private set; }
        public IProductType ProductType { get; private set; }
        public IProductImage ProductImage { get; private set; }

        public IOrderDetails orderDetails { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
