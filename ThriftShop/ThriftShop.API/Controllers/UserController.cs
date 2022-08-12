using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork service)
        {
            this.unitOfWork = service;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int id)
        {
            var model = await unitOfWork.UserInfo.GetFirstOrDefault(uf => uf.AccountID.Equals(id));
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo()
        {
            var model = await unitOfWork.UserInfo.GetAll();
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostUser(UserInfo user)
        {
            if (user != null)
            {
                await unitOfWork.UserInfo.Add(user);
                unitOfWork.Save();
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<ActionResult<UserInfo>> Update(UserInfo user)
        {
            if (user != null)
            {
                await unitOfWork.UserInfo.Update(user);
                unitOfWork.Save();
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult<UserInfo>> Delete(IEnumerable<UserInfo> user)
        {
            if (user != null)
            {
                unitOfWork.UserInfo.RemoveRange(user);
                unitOfWork.Save();
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
