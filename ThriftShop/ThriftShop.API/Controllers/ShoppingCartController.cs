using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWord;
        public ShoppingCartController(IUnitOfWork _unitOfWord)
        {
            this._unitOfWord = _unitOfWord;
        }

        [HttpGet]
        public async Task<IEnumerable<ShoppingCart>> GetListCart() {
            return await _unitOfWord.ShoppingCart.GetAll(includeProperties: "UserInfo,Product");
        }

        [HttpGet("{id}")]
        public async Task<ShoppingCart> GetCart(int id)
        {
            return await _unitOfWord.ShoppingCart.GetFirstOrDefault(x=>x.Id.Equals(id),includeProperties: "UserInfo,Product");
        }

        [HttpPost]
        public async Task<ShoppingCart> PostCart(ShoppingCart obj)
        {
            var model = await _unitOfWord.ShoppingCart.GetFirstOrDefault(x => x.Id.Equals(obj),includeProperties:"UserInfo,Product");
            if (model != null)
            {
                model.Count += 1;
                await _unitOfWord.ShoppingCart.Update(model);
                _unitOfWord.Save();
                return model;
            }
            else {
                await _unitOfWord.ShoppingCart.Add(obj);
                _unitOfWord.Save();
                return obj;
            }
            
        }

        [HttpPut]
        public async Task<ShoppingCart> PutCart(ShoppingCart obj)
        {
            await _unitOfWord.ShoppingCart.Update(obj);
            _unitOfWord.Save();
            return obj;
        }

        [HttpDelete]
        public async Task<ShoppingCart> DeleteCart(int id)
        {
            var model = await _unitOfWord.ShoppingCart.GetFirstOrDefault(x=>x.Id.Equals(id),includeProperties: "UserInfo,Product");
            _unitOfWord.ShoppingCart.Remove(model);
            _unitOfWord.Save();
            return model;
        }
    }
}
