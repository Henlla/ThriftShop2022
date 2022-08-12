using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //Find All

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await unitOfWork.Product.GetAll(includeProperties: "Category"));
        }

        //Find by ID

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetFirstOrDefault(int id)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == id);
            if (_product != null)
            {
                return Ok(_product);
            }
            return BadRequest();
        }


        //Filter by Category ID
        [HttpGet("/GetAllProductByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductByCategory(int categoryId)
        {
            return Ok(await unitOfWork.Product.GetAll(x=>x.CategoryId == categoryId,includeProperties:"Category"));
        }

        //Filter by Created Date 
        //[HttpGet("/GetAllProductByCreatedDate/{date}")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAllProductByCategory(int categoryId)
        //{
        //    return Ok(await unitOfWork.Product.GetAll(x => x.CategoryId == categoryId, includeProperties: "Category"));
        //}

        //Filter by Price


        [HttpGet("/GetAllProductByPrice/{fromPrice}/{toPrice}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductByPrice(double fromPrice, double toPrice)
        {
            return Ok(await unitOfWork.Product.GetAll(x => x.Price >= fromPrice && x.Price <= toPrice, includeProperties: "Category"));
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == product.ProductId);
            if (_product == null)
            {
                await unitOfWork.Product.Add(product);
                unitOfWork.Save();
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == product.ProductId);
            if (_product != null)
            {
                await unitOfWork.Product.Update(product);
                unitOfWork.Save();
                return Ok(product);
            }
            return BadRequest();

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Product product)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == product.ProductId);
            if (_product != null)
            {
                unitOfWork.Product.Remove(product);
                unitOfWork.Save();
                return Ok(product);
            }
            return BadRequest();
        }
    }
}

