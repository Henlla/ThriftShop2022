using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ProductImagesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            return await unitOfWork.ProductImage.GetAll();
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetFirstOrDefault(int id)
        {
            var _cate = await unitOfWork.ProductImage.GetFirstOrDefault(x=>x.ProductId == id);
            if(_cate != null)
            {
                return Ok(_cate);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<ProductImage>> AddCategory(ProductImage producImage)
        {
            var _cate = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == producImage.ProductId);
            if(_cate == null)
            {
                await unitOfWork.ProductImage.Add(producImage);
                unitOfWork.Save();
                return Ok(producImage);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductImage(ProductImage producImage)
        {
                await unitOfWork.ProductImage.Update(producImage);
                unitOfWork.Save();
                return Ok(producImage);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductImage(ProductImage producImage)
        {
            var _productImage = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == producImage.ProductId);
            if (_productImage != null)
            {
                unitOfWork.ProductImage.Remove(producImage);
                unitOfWork.Save();
                return Ok(producImage);
            }
            return BadRequest();
        }
    }
}
