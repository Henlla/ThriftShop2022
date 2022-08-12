using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ProductTypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductType>> GetAll()
        {
            return await unitOfWork.ProductType.GetAll();
        }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetFirstOrDefault(int id)
        {
            var _cate = await unitOfWork.ProductType.GetFirstOrDefault(x=>x.ProductTypeId == id);
            if(_cate != null)
            {
                return Ok(_cate);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddProductType(ProductType productType)
        {
            var _productType = await unitOfWork.ProductType.GetFirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
            if(_productType == null)
            {
                await unitOfWork.ProductType.Add(productType);
                unitOfWork.Save();
                return Ok(productType);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductType(ProductType productType)
        {
                await unitOfWork.ProductType.Update(productType);
                unitOfWork.Save();
                return Ok(productType);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProductType(ProductType productType)
        {
            var _cate = await unitOfWork.ProductType.GetFirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
            if (_cate != null)
            {
                unitOfWork.ProductType.Remove(productType);
                unitOfWork.Save();
                return Ok(productType);
            }
            return BadRequest();
        }
    }
}
