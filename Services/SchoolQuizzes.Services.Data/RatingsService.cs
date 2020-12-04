namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingRepository;

        public RatingsService(IRepository<Rating> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public double GetAverageRatings(int questionId)
        {
            return this.ratingRepository.All()
                .Where(x => x.QuestionId == questionId)
                .Average(x => x.Value);
        }

        public async Task SetRatingAsync(int questionId, string userId, int value)
        {
            var rating = this.ratingRepository.All()
               .FirstOrDefault(x => x.QuestionId == questionId && x.UserId == userId);

            if (rating == null)
            {
                rating = new Rating
                {
                    QuestionId = questionId,
                    UserId = userId,
                };

                await this.ratingRepository.AddAsync(rating);
            }

            rating.Value = value;
            await this.ratingRepository.SaveChangesAsync();
        }
    }
}
