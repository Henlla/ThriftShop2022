using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Color_ProductsController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public Color_ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Color_Product>> GetAll()
        {
            return await unitOfWork.Color_Product.GetAll();
        }

      

        [HttpGet("{colorId}/{productId}")]
        public async Task<ActionResult<Color_Product>> GetFirstOrDefault(int colorId, int productId)
        {
            var _color_Product = await unitOfWork.Color_Product.GetFirstOrDefault(x => x.ColorId == colorId && x.ProductId == productId);
            if(_color_Product != null)
            {
                return Ok(_color_Product);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Color_Product>> AddColor_Product(Color_Product color_Product)
        {
            var _color_Product = await unitOfWork.Color_Product.GetFirstOrDefault(x => x.ColorId == color_Product.ColorId && x.ProductId == color_Product.ProductId);
            if(_color_Product == null)
            {
                await unitOfWork.Color_Product.Add(color_Product);
                unitOfWork.Save();
                return Ok(color_Product);
            }
            return BadRequest();
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteColor_Product(Color_Product color_Product)
        {
            var _color_Product = await unitOfWork.Color_Product.GetFirstOrDefault(x => x.ColorId == color_Product.ColorId && x.ProductId == color_Product.ProductId);
            if (_color_Product != null)
            {
                unitOfWork.Color_Product.Remove(color_Product);
                unitOfWork.Save();
                return Ok(color_Product);
            }
            return BadRequest();
        }
    }
}
