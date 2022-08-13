using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.DataAccess.Repository.IRepository;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWord;


        public OrderDetailsController(IUnitOfWork _unitOfWord)
        {
            this._unitOfWord = _unitOfWord;
        }


        [HttpGet("{orderId}")]
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId)
        {

            var model = await _unitOfWord.orderDetails.GetAll(od => od.OrderId.Equals(orderId), includeProperties: "Product");
            return model;
        }
        [HttpPost]
        public async Task<OrderDetail> PostOrderDetail(OrderDetail obj)
        {
            var model = await _unitOfWord.orderDetails.GetFirstOrDefault(od => od.Id.Equals(obj.Id));
            if (model == null)
            {
                await _unitOfWord.orderDetails.Add(obj);
                _unitOfWord.Save();
                return obj;
            }
            else { return null; }
            return model;

        }



    }
}
