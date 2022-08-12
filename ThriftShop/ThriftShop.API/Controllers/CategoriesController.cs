using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await unitOfWork.Category.GetAll();
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetFirstOrDefault(int id)
        {
            var _cate = await unitOfWork.Category.GetFirstOrDefault(x=>x.CategoryId == id);
            if(_cate != null)
            {
                return Ok(_cate);
            }
            return BadRequest();
        }

        [HttpPost("{Size}")]
        public async Task<ActionResult<Category>> AddCategory(Category category,string Size)
        {
            var _cate = await unitOfWork.Category.GetFirstOrDefault(x => x.CategoryId == category.CategoryId);
            if(_cate == null)
            {
                await unitOfWork.Category.Add(category);
                unitOfWork.Save();
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
                await unitOfWork.Category.Update(category);
                unitOfWork.Save();
                return Ok(category);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(Category category)
        {
            var _cate = await unitOfWork.Category.GetFirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (_cate != null)
            {
                unitOfWork.Category.Remove(category);
                unitOfWork.Save();
                return Ok(category);
            }
            return BadRequest();
        }
    }
}
