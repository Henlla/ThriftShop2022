using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public StocksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> GetAll()
        {
            return await unitOfWork.Stock.GetAll();
        }

      

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetFirstOrDefault(int id)
        {
            var _stock = await unitOfWork.Stock.GetFirstOrDefault(x=>x.ProductId == id);
            if(_stock != null)
            {
                return Ok(_stock);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> AddStock(Stock stock)
        {
            var _stock = await unitOfWork.Stock.GetFirstOrDefault(x => x.ProductId == stock.ProductId);
            if(_stock == null)
            {
                await unitOfWork.Stock.Add(stock);
                unitOfWork.Save();
                return Ok(stock);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStock(Stock stock)
        {
                await unitOfWork.Stock.Update(stock);
                unitOfWork.Save();
                return Ok(stock);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(Stock stock)
        {
            var _stock = await unitOfWork.Stock.GetFirstOrDefault(x => x.ProductId == stock.ProductId);
            if (_stock != null)
            {
                unitOfWork.Stock.Remove(stock);
                unitOfWork.Save();
                return Ok(stock);
            }
            return BadRequest();
        }
    }
}
