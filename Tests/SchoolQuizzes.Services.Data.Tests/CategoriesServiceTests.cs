namespace SchoolQuizzes.Services.Data.Tests
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;

    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void TestGetCategoryNameById()
        {
            string expectedValue = "Математика";

            var list = new List<Category>() { new Category() { Id = 1, Name = expectedValue }, new Category() { Id = 2, Name = "Информатика" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => list.Add(category));

            var service = new CategoriesService(mockRepo.Object);

            string actual = service.GetCategoryNameById(1);

            Assert.Equal(expectedValue, actual);
        }

        [Fact]
        public void TestGetAllAsSelectList()
        {
            var list = new List<Category>() { new Category() { Id = 1, Name = "Математика" }, new Category() { Id = 2, Name = "Информатика" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => list.Add(category));

            var service = new CategoriesService(mockRepo.Object);

            SelectList expectedList = new SelectList(list, "Id", "Name");

            SelectList actual = service.GetAllAsSelectList();

            Assert.Equal(expectedList.Items, actual.Items);
        }

        [Fact]
        public void ТестGetAllAsKeyValuePairs()
        {
            var list = new List<Category>() { new Category() { Id = 1, Name = "Математика" }, new Category() { Id = 2, Name = "Информатика" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => list.Add(category));

            var service = new CategoriesService(mockRepo.Object);

            var expectedList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("1","Математика"),
                new KeyValuePair<string, string>("2","Информатика"),
            };

            var actual = service.GetAllAsKeyValuePairs();

            Assert.Equal(expectedList, actual);
        }
    }
}
