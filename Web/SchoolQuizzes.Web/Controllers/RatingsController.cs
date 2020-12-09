namespace SchoolQuizzes.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Ratings;

    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : BaseController
    {
        private readonly IRatingsService ratingService;

        public RatingsController(IRatingsService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<PostRatingResponseModel>> Post(PostRatingInputModel input)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.ratingService.SetRatingAsync(input.QuestionId, userId, input.Value);
            double averageRating = this.ratingService.GetAverageRatings(input.QuestionId);
            return new PostRatingResponseModel { AverageRating = averageRating };
        }
    }
}
