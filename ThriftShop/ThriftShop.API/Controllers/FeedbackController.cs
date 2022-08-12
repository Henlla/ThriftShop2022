using ThriftShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWord;
        public FeedbackController(IUnitOfWork _unitOfWord)
        {
            unitOfWord = _unitOfWord;
        }
        [HttpGet]
        public async Task<IEnumerable<Feedback>> ListFeedback() {
            return await unitOfWord.Feedback.GetAll(includeProperties: "UserInfo");
        }

        [HttpPost]
        public async Task<Feedback> PostFeedback(Feedback obj)
        {
            await unitOfWord.Feedback.Add(obj);
            unitOfWord.Save();
            return obj;
        }
        [HttpPut]
        public async Task<Feedback> PutFeedback(Feedback obj)
        {
            var model = unitOfWord.Feedback.GetFirstOrDefault(x => x.FeedbackId.Equals(obj.FeedbackId), includeProperties: "UserInfo");
            if (model != null) {
                await unitOfWord.Feedback.Update(obj);
                unitOfWord.Save();
                return obj;
            }
            else {
                return null;
            }
        }
    }
}
