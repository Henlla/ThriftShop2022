using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAcccountController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public UserAcccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<UserAccount>> Login(string username, string password)
        {
            var userAccount = await unitOfWork.UserAccount.GetFirstOrDefault(ua => ua.Username.Equals(username));
            if (userAccount != null)
            {
                var pass = BCrypt.Net.BCrypt.Verify(password, userAccount.Password);
                if (pass)
                {
                    return Ok(userAccount);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserAccount>> Register(UserAccount user)
        {
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await unitOfWork.UserAccount.Add(user);
                unitOfWork.Save();
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
