namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        public Task SetRatingAsync(int questionId, string userId, int value);

        public double GetAverageRatings(int questionId);
    }
}
