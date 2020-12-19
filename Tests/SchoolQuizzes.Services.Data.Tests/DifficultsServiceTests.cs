namespace SchoolQuizzes.Services.Data.Tests
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;

    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public  class DifficultsServiceTests
    {
        [Fact]
        public void TestGetDifficultNameById()
        {
            string expectedValue = "Лесно";

            var list = new List<Difficult>() { new Difficult() { Id = 1, Name = expectedValue }, new Difficult() { Id = 2, Name = "Трудно" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Difficult>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Difficult>())).Callback(
                (Difficult difficult) => list.Add(difficult));

            var service = new DifficultsService(mockRepo.Object);

            string actual = service.GetDifficultNameById(1);

            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public void TestGetAllAsSelectList()
        {
            var list = new List<Difficult>() { new Difficult() { Id = 1, Name = "Лесно" }, new Difficult() { Id = 2, Name = "Трудно" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Difficult>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Difficult>())).Callback(
                (Difficult difficult) => list.Add(difficult));

            var service = new DifficultsService(mockRepo.Object);

            SelectList expectedList = new SelectList(list, "Id", "Name");

            SelectList actual = service.GetAllAsSelectList();

            Assert.Equal(expectedList.Items, actual.Items);
        }

        [Fact]
        public void ТестGetAllAsKeyValuePairs()
        {
            var list = new List<Difficult>() { new Difficult() { Id = 1, Name = "Лесно" }, new Difficult() { Id = 2, Name = "Трудно" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Difficult>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Difficult>())).Callback(
                (Difficult difficult) => list.Add(difficult));

            var service = new DifficultsService(mockRepo.Object);

            var expectedList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("1","Лесно"),
                new KeyValuePair<string, string>("2","Трудно"),
            };

            var actual = service.GetAllAsKeyValuePairs();

            Assert.Equal(expectedList, actual);
        }
    }
}
