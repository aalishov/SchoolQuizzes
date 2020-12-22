namespace SchoolQuizzes.Services.Data.Tests
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class StagesServiceTests
    {
        [Fact]
        public void TestGetAllAsSelectList()
        {
            var list = new List<Stage>() { new Stage() { Id = 1, Name = "1 клас" }, new Stage() { Id = 2, Name = "2 клас" } };
            var mockRepo = new Mock<IDeletableEntityRepository<Stage>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Stage>())).Callback((Stage s) => list.Add(s));

            var service = new StagesService(mockRepo.Object);

            SelectList expectedList = new SelectList(list, "Id", "Name");

            SelectList actual = service.GetAllAsSelectList();

            Assert.Equal(expectedList.Items, actual.Items);
        }
    }
}
