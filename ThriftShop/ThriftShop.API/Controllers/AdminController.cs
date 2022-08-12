using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<Admin>> Login(string username,string password)
        {
            var account = await unitOfWork.Admin.GetFirstOrDefault(acc => acc.Username.Equals(username));
            if (account != null)
            {
                var getPass = BCrypt.Net.BCrypt.Verify(password,account.Password);
                if (getPass)
                {
                    return Ok(account);
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
        public async Task<ActionResult<Admin>> Register(Admin admin)
        {
            if (admin != null)
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                await unitOfWork.Admin.Add(admin);
                unitOfWork.Save();
                return Ok(admin);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
