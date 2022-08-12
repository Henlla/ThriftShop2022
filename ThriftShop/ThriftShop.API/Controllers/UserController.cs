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
        [HttpGet("id")]
        public async Task<UserInfo> GetUserInfo(int id)
        {
            return await unitOfWork.UserInfo.GetFirstOrDefault(uf=>uf.AccountID == id);
        }
        [HttpGet]
        public async Task<IEnumerable<UserInfo>> GetUserInfo()
        {
            return await unitOfWork.UserInfo.GetAll();
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
