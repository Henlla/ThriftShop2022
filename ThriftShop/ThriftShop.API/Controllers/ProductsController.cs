using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;
using ThriftShop.Utility;
using ThriftShop.Models.PageModel;

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
        [HttpGet("GetAll/{keyword?}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(string? keyword = null)
        {
            if (keyword != null)
            {
                return Ok(await unitOfWork.Product.GetAll(x =>
                    x.Category.CategoryName.Contains(keyword) ||
                    x.Brand.Contains(keyword),
                   includeProperties: "Category,Size_Product,Color_Product,ProductImage"));
            }
            var model = await unitOfWork.Product.GetAll(includeProperties: "Category,Size_Product,Color_Product,ProductImage");
       
            return Ok(model);

        }




        //Pagination 

        [HttpGet("paginate/{page}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductPagination(int page)
        {
            if (unitOfWork.Product == null)
            {
                return NotFound();
            }
            var numberOfProduct = await unitOfWork.Product.GetAll(includeProperties: "Category,Size,Color,ProductImage");
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
            return Ok(await unitOfWork.Product.GetAll(x => x.CategoryId == categoryId, includeProperties: "Category,Size,Color,ProductImage"));
        }

        //Filter by Brand
        [HttpGet("/GetProductByBrand/{brandName}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(string brandName)
        {
            var model = await unitOfWork.Product.GetAll(x => x.Brand == brandName, includeProperties: "Category,Size,Color,ProductImage");
            return Ok(model);
        }

        //Filter by Created Date 
        [HttpGet("/GetAllProductByCreatedDate/{createdDateFromClient}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCreatedDate(string createdDateFromClient) // (yyyy-MM-dd format)
        {
            //var dateTime = DateOnly.ParseExact(createdDateFromClient,"yyyy-MM-dd",null);
            return Ok(await unitOfWork.Product.GetAll(x => x.CreatedDate.ToString() == createdDateFromClient, includeProperties: "Category,Size,Color,ProductImage"));
        }

        //Filter by Price


        [HttpGet("/GetProductByPrice/{fromPrice}/{toPrice}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(double fromPrice, double toPrice)
        {
            return Ok(await unitOfWork.Product.GetAll(x => x.Price >= fromPrice && x.Price <= toPrice, includeProperties: "Category,Size,Color,ProductImage"));
        }

        

        [HttpPost()]
        public async Task<ActionResult<Product>> AddProduct(ProductVM productVM)
        {
            var _product = await unitOfWork.Product.GetFirstOrDefault(x => x.ProductId == productVM.Product.ProductId);
            if (_product == null)
            {
                productVM.Product.ProductImage = null;
                 await unitOfWork.Product.Add(productVM.Product);
                unitOfWork.Save(); 
            }
            else
            {
                return BadRequest();
            }
            
            if (productVM.Size != null && productVM.Color != null)
            {
                var product_list = await unitOfWork.Product.GetAll();
              
                var lastedProductInDB = product_list.OrderByDescending(x=>x.ProductId).First();
                if (lastedProductInDB != null)
                {
                    foreach (var item in productVM.Size)
                    {
                        Size_Product size_Product = new Size_Product()
                        {
                            SizeId = item.SizeId,
                            SizeType = item.SizeType,
                            ProductId = lastedProductInDB.ProductId,
                        };

                        await unitOfWork.Size_Product.Add(size_Product);
                        unitOfWork.ClearChangeTracker();
                    }
                    
                    unitOfWork.Save();


                    foreach (var item in productVM.Color)
                    {
                        Color_Product color_Product = new Color_Product()
                        {
                            ColorId = item.ColorId,
                            ColorType = item.ColorType,
                            ProductId = lastedProductInDB.ProductId,
                        };
                        await unitOfWork.Color_Product.Add(color_Product);
                        unitOfWork.ClearChangeTracker();
                    }

                    unitOfWork.Save();

                    //productVM.Product.JsonImageList = "123";
                    if (productVM.Product.ImageList == null)
                    {
                        List<string> list = new List<string>()
                        {
                            "image1.png",
                            "image2.png",
                            "image3.png",
                            "image4.png",
                            "image5.png",
                            "image6.png",
                        };
                        //check if number of image <= 6, maximum is 6
                        //var ListImageFromDB_ByProductId = await unitOfWork.ProductImage.GetAll(x => x.ProductId == productVM.Product.ProductId);
                        //var numberImageFromDB = ListImageFromDB_ByProductId.Count();
                        //var numberImageInList = list.Count();
                        //var NumberOfImageCanUpload = numberImageFromDB - numberImageInList;

                        var productIsExistImage = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == productVM.Product.ProductId);
                        if (productIsExistImage == null)
                        {
                            foreach (var item in list)
                            {
                                ProductImage productImage = new ProductImage()
                                {
                                    ImageUrl = item.ToString(),
                                    ProductId = lastedProductInDB.ProductId,
                                    IsMainImage = false,
                                };
                                await unitOfWork.ProductImage.Add(productImage);
                            };
                            unitOfWork.Save();
                            //Set first picture is main image
                            var _productImage = await unitOfWork.ProductImage.GetFirstOrDefault(x => x.ProductId == lastedProductInDB.ProductId);
                            _productImage.IsMainImage = true;
                            unitOfWork.Save();
                        }
                    }
                }
            }
            return Ok(productVM.Product);

            
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
            else
            {
                return BadRequest();
            }
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

