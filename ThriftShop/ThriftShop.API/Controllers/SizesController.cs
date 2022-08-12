using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SizesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Size>> GetAll()
        {
            return await unitOfWork.Size.GetAll();
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetFirstOrDefault(int id)
        {
            var _cate = await unitOfWork.Size.GetFirstOrDefault(x=>x.SizeId == id);
            if(_cate != null)
            {
                return Ok(_cate);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Size>> AddSize(Size size)
        {
            var _cate = await unitOfWork.Size.GetFirstOrDefault(x => x.SizeId == size.SizeId);
            if(_cate == null)
            {
                await unitOfWork.Size.Add(size);
                unitOfWork.Save();
                return Ok(size);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSize(Size size)
        {
                await unitOfWork.Size.Update(size);
                unitOfWork.Save();
                return Ok(size);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSize(Size size)
        {
            var _cate = await unitOfWork.Size.GetFirstOrDefault(x => x.SizeId == size.SizeId);
            if (_cate != null)
            {
                unitOfWork.Size.Remove(size);
                unitOfWork.Save();
                return Ok(size);
            }
            return BadRequest();
        }
    }
}
