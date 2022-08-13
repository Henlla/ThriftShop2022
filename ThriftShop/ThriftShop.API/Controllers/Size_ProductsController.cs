using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Size_ProductsController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public Size_ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Size_Product>> GetAll()
        {
            return await unitOfWork.Size_Product.GetAll();
        }



        [HttpGet("{sizeId}/{productId}")]
        public async Task<ActionResult<Size_Product>> GetFirstOrDefault(int sizeId, int productId)
        {
            var _size_Product = await unitOfWork.Size_Product.GetFirstOrDefault(x => x.SizeId == sizeId && x.ProductId == productId);
            if (_size_Product != null)
            {
                return Ok(_size_Product);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Size_Product>> AddSize_Product(Size_Product size_Product)
        {
            var _size_Product = await unitOfWork.Size_Product.GetFirstOrDefault(x => x.SizeId == size_Product.SizeId && x.ProductId == size_Product.ProductId);
            if (_size_Product == null)
            {
                await unitOfWork.Size_Product.Add(size_Product);
                unitOfWork.Save();
                return Ok(size_Product);
            }
            return BadRequest();
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteSize_Product(Size_Product size_Product)
        {
            var _size_Product = await unitOfWork.Size_Product.GetFirstOrDefault(x => x.SizeId == size_Product.SizeId && x.ProductId == size_Product.ProductId);
            if (_size_Product != null)
            {
                unitOfWork.Size_Product.Remove(size_Product);
                unitOfWork.Save();
                return Ok(size_Product);
            }
            return BadRequest();
        }
    }
}
