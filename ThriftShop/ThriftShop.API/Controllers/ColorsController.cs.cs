using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ColorsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Color>> GetAll()
        {
            return await unitOfWork.Color.GetAll();
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetFirstOrDefault(int id)
        {
            var _color = await unitOfWork.Color.GetFirstOrDefault(x=>x.ColorId == id);
            if(_color != null)
            {
                return Ok(_color);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Color>> AddColor(Color color)
        {
            var _color = await unitOfWork.Color.GetFirstOrDefault(x => x.ColorId == color.ColorId);
            if(_color == null)
            {
                await unitOfWork.Color.Add(color);
                unitOfWork.Save();
                return Ok(color);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateColor(Color color)
        {
                await unitOfWork.Color.Update(color);
                unitOfWork.Save();
                return Ok(color);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteColor(Color color)
        {
            var _color = await unitOfWork.Color.GetFirstOrDefault(x => x.ColorId == color.ColorId);
            if (_color != null)
            {
                unitOfWork.Color.Remove(color);
                unitOfWork.Save();
                return Ok(color);
            }
            return BadRequest();
        }
    }
}
