namespace SchoolQuizzes.Services.Data.Tests
{
    using Moq;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;

    public class RatingsServiceTests
    {
        [Fact]
        public async Task WhenUserRate2TimesOnly1RateeShouldBeCounted()
        {
            var list = new List<Rating>();
            var mockRepo = new Mock<IRepository<Rating>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback(
                (Rating rating) => list.Add(rating));
            var service = new RatingsService(mockRepo.Object);

            await service.SetRatingAsync(1, "1", 1);
            await service.SetRatingAsync(1, "1", 5);
            await service.SetRatingAsync(1, "1", 5);
            await service.SetRatingAsync(1, "1", 5);
            await service.SetRatingAsync(1, "1", 5);

            Assert.Equal(1, list.Count);
            Assert.Equal(5, list.First().Value);
        }

        [Fact]
        public async Task When2UsersRateForTheSameQuestionTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Rating>();
            var mockRepo = new Mock<IRepository<Rating>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback(
                (Rating rating) => list.Add(rating));
            var service = new RatingsService(mockRepo.Object);

            await service.SetRatingAsync(2, "Niki", 5);
            await service.SetRatingAsync(2, "Pesho", 1);
            await service.SetRatingAsync(2, "Niki", 2);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Rating>()), Times.Exactly(2));

            Assert.Equal(2, list.Count);
            Assert.Equal(1.5, service.GetAverageRatings(2));
        }
    }
}
