using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CouponController(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        [HttpGet]
        public async Task<IEnumerable<Coupon>> GetCoupons()
        {
            return await _unitOfWork.Coupon.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Coupon> GetCoupon(int id)
        {
            return await _unitOfWork.Coupon.GetFirstOrDefault(x => x.CouponId.Equals(id));
        }

        [HttpPost]
        public async Task<Coupon> PostCoupon(Coupon obj)
        {
            await _unitOfWork.Coupon.Add(obj);
            _unitOfWork.Save();
            return obj;
        }

        [HttpPut]
        public async Task<Coupon> PutCoupon(Coupon obj)
        {
            var model = await _unitOfWork.Coupon.GetFirstOrDefault(x => x.CouponId.Equals(obj.CouponId));
            if (model != null)
            {
                model.Code = obj.Code;
                model.CouponType = obj.CouponType;
                model.DiscountValue = obj.DiscountValue;
                model.ExpiredDate = obj.ExpiredDate;
                model.Quantity = obj.Quantity;
                await _unitOfWork.Coupon.Update(obj);
            }
            _unitOfWork.Save();
            return obj;
        }


       



        [HttpDelete]
        public async Task<Coupon> DeleteCoupon(int id)
        {
            var model = await _unitOfWork.Coupon.GetFirstOrDefault(x => x.CouponId.Equals(id));
            _unitOfWork.Coupon.Remove(model);
            _unitOfWork.Save();
            return model;
        }

    }
}
