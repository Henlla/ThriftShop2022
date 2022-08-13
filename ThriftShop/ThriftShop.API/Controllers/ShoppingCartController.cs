using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;
using ThriftShop.Models.ModelVirtual;
using System.Web.Http;

namespace ThriftShop.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWord;
        public ShoppingCartController(IUnitOfWork _unitOfWord)
        {
            this._unitOfWord = _unitOfWord;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IEnumerable<ShoppingCart>> GetListCart()
        {
            return await _unitOfWord.ShoppingCart.GetAll(includeProperties: "UserInfo,Product");
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ShoppingCart> GetCart(int id)
        {
            return await _unitOfWord.ShoppingCart.GetFirstOrDefault(x => x.Id.Equals(id), includeProperties: "UserInfo,Product");
        }

       
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ShoppingCart> PostCart(ShoppingCart obj)
        {
            var model = await _unitOfWord.ShoppingCart.GetFirstOrDefault(x => x.ProductId.Equals(obj.ProductId), includeProperties: "UserInfo,Product");
            if (model != null)
            {
                model.Count = obj.Count;
                await _unitOfWord.ShoppingCart.Update(model);
                _unitOfWord.Save();
                return obj;
            }
            await _unitOfWord.ShoppingCart.Add(obj);
            _unitOfWord.Save();
            return obj;
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        public async Task<ShoppingCart> PutCart(ShoppingCart obj)
        {
            var model = await _unitOfWord.ShoppingCart.GetFirstOrDefault(x => x.Id.Equals(obj.Id), includeProperties: "UserInfo,Product");
            if (obj.Action == "plus")
            {
                if (model != null)
                {
                    model.Count += 1;
                    await _unitOfWord.ShoppingCart.Update(model);
                    _unitOfWord.Save();
                    return model;
                }
            }
            else
            {
                if (model != null)
                {
                    model.Count -= 1;
                    await _unitOfWord.ShoppingCart.Update(model);
                    _unitOfWord.Save();
                    return model;
                }
            }
            return null;
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ShoppingCart> DeleteCart(int id)
        {
            var model = await _unitOfWord.ShoppingCart.GetFirstOrDefault(x => x.Id.Equals(id), includeProperties: "UserInfo,Product");
            _unitOfWord.ShoppingCart.Remove(model);
            _unitOfWord.Save();
            return model;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("GetAll/{userId}")]
        public async Task<IEnumerable<ShoppingCart>> GetListCartByUserId(int userId)
        {
            return await _unitOfWord.ShoppingCart.GetAll(sp => sp.UserId.Equals(userId), includeProperties: "UserInfo,Product");
        }

    }
}
