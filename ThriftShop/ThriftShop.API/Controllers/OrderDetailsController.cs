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
        public Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId)
        {
            return _unitOfWord.orderDetails.GetAll(od=>od.OrderId.Equals(orderId),includeProperties: "Product");
        }
        [HttpPost]
        public async Task<IEnumerable<OrderDetail>> PostOrderDetail(Order obj)
        {
          
          IEnumerable<ShoppingCart> shoppingCartList = await _unitOfWord.ShoppingCart.GetAll(sp => sp.UserId.Equals(obj.UserId),includeProperties: "Product");
            if (shoppingCartList != null)
            {
                List<OrderDetail> orderDetaiList = new List<OrderDetail>();
                foreach (var item in shoppingCartList)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = obj.Id;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.Count = item.Count;
                    orderDetail.Price = item.Product.Price;
                    orderDetaiList.Add(orderDetail);

                }
                await _unitOfWord.orderDetails.AddRange(orderDetaiList);
                _unitOfWord.ShoppingCart.RemoveRange(shoppingCartList);
                _unitOfWord.Save();
                return orderDetaiList;
            }
            else {

                return null;
            
            }
        
        }



    }
}
