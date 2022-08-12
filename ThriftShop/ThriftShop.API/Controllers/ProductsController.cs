using ThriftShop.DataAccess.Repository.IRepository;
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
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(string? keyword)
        {
            if(keyword != null)
            {
                return Ok(await unitOfWork.Product.GetAll(x =>
                x.Category.CategoryName.Contains(keyword) ||
                x.Brand.Contains(keyword),
               includeProperties: "Category,Size,Color,ProductType,ProductImage"));
            }
            return Ok(await unitOfWork.Product.GetAll(includeProperties: "Category,Size,Color,ProductType,ProductImage"));

        }




        //Pagination 

        [HttpGet("paginate/{page}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductPagination(int page)
        {
            if(unitOfWork.Product == null)
            {
                return NotFound();
            }
            var numberOfProduct = await unitOfWork.Product.GetAll(includeProperties: "Category,Size,Color,ProductType,ProductImage");
            var pageResult = 5f; //number of product
            var pageCount = Math.Ceiling(numberOfProduct.Count() / pageResult);

            var products = numberOfProduct
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
            var response = new ProductResponse()
            {
                Products = products,
                CurrentPages = page,
                Pages = (int)pageCount
            };
            return Ok(response);
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
        [HttpGet("/GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int categoryId)
        {
            return Ok(await unitOfWork.Product.GetAll(x=>x.CategoryId == categoryId,includeProperties: "Category,Size,Color,ProductType,ProductImage"));
        }

        //Filter by Brand
        [HttpGet("/GetProductByBrand/{brandName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(string brandName)
        {
            var model = await unitOfWork.Product.GetAll(x => x.Brand == brandName, includeProperties: "Category,Size,Color,ProductType,ProductImage");
            return Ok(model);
        }

        //Filter by Created Date 
        [HttpGet("/GetAllProductByCreatedDate/{createdDateFromClient}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCreatedDate(string createdDateFromClient) // (yyyy-MM-dd format)
        {
            //var dateTime = DateOnly.ParseExact(createdDateFromClient,"yyyy-MM-dd",null);
            return Ok(await unitOfWork.Product.GetAll(x => x.CreatedDate.ToString() == createdDateFromClient, includeProperties: "Category,Size,Color,ProductType,ProductImage"));
        }

        //Filter by Price


        [HttpGet("/GetProductByPrice/{fromPrice}/{toPrice}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(double fromPrice, double toPrice)
        {
            return Ok(await unitOfWork.Product.GetAll(x => x.Price >= fromPrice && x.Price <= toPrice, includeProperties: "Category,Size,Color,ProductType,ProductImage"));
        }
        
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product, string? jsonListImage)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == product.ProductId);
            if (_product == null)
            {
                //product.ProductImage = null;
                await unitOfWork.Product.Add(product);
                unitOfWork.Save();
            }
            else
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(jsonListImage))
            {
                //var list = jsonListImage.ToList();
                List<string> list = new List<string>()
                {
                    "image1.png",
                    "image2.png",
                    "image3.png",
                    "image4.png",
                    "image5.png",
                    "image6.png",
                };

                var productIsExistImage = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == product.ProductId);
                if (productIsExistImage == null)
                {
                    foreach (var item in list)
                    {
                        ProductImage productImage = new ProductImage()
                        {
                            ImageUrl = item.ToString(),
                            ProductId = product.ProductId,
                            IsMainImage = false,
                        };
                        await unitOfWork.ProductImage.Add(productImage);
                    };
                    unitOfWork.Save();
                    //Set first picture is main image
                    var _productImage = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == product.ProductId);
                    _productImage.IsMainImage = true;
                    unitOfWork.Save();
                }
            }
            //string JsonImageList = "[{'image1.png','image3.png','image4.png','image5.png'}]";

            return Ok(product);

            
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

