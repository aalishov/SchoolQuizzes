﻿namespace SchoolQuizzes.Services.Data.Tests
{
    using Moq;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Xunit;

    [Collection("Serial")]
    public class AnswersServiceTests
    {
        public AnswersServiceTests()
        {
            
        }

        [Fact]
        public void TestGetAnswerValueById()
        {
            var list = new List<Answer>()
            {
                new Answer() { Id = 1, Value="1" },
                new Answer() { Id = 2, Value="2" },
                new Answer() { Id = 3, Value="3" },
                new Answer() { Id = 4, Value="4" },
                new Answer() { Id = 5, Value="5" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Answer>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Answer>())).Callback(
                (Answer answer) => list.Add(answer));

            var service = new AnswersService(mockRepo.Object);

            var expected = "2";

            var actual = service.GetAnswerValueById(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetGetQuestionAnswerById()
        {
            var listQuestionAnswer1 = new List<QuestionAnswer>()
            {
                new QuestionAnswer() { AnswerId=1,QuestionId=1},
            };
            var listQuestionAnswer2 = new List<QuestionAnswer>()
            {
                new QuestionAnswer() { AnswerId=2,QuestionId=1},
            };
            var list = new List<Answer>()
            {
                new Answer() { Id = 1, Value="1",Questions=listQuestionAnswer1 },
                new Answer() { Id = 2, Value="2",Questions=listQuestionAnswer2 },
                new Answer() { Id = 3, Value="3" },
                new Answer() { Id = 4, Value="4" },
                new Answer() { Id = 5, Value="5" },
            };
            var listQuestions = new List<Question>()
            {
                new Question() { Id=1},
                new Question() { Id=2},
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Answer>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Answer>())).Callback(
                (Answer answer) => list.Add(answer));


            var service = new AnswersService(mockRepo.Object);

            var expected = new List<AnswerInTestModel>()
            {
                new AnswerInTestModel() { Id=1,Value="1"},
                new AnswerInTestModel() { Id=2,Value="2"},
            };
            AutoMapperConfig.RegisterMappings(typeof(AnswerInTestModel).GetTypeInfo().Assembly);
            var actual = service.GetQuestionAnswersById<AnswerInTestModel>(1);

            Assert.Equal(string.Join(" ",expected), string.Join(" ",actual));
        }



        [Fact]
        public void TestGetQuestionAnswersForTakesById()
        {
            var listQuestionAnswer1 = new List<QuestionAnswer>()
            {
                new QuestionAnswer() { AnswerId=1,QuestionId=2},
            };
            var listQuestionAnswer2 = new List<QuestionAnswer>()
            {
                new QuestionAnswer() { AnswerId=2,QuestionId=1},
            };
            var listQuestionAnswer3 = new List<QuestionAnswer>()
            {
                new QuestionAnswer() { AnswerId=4,QuestionId=2},
            };
            var list = new List<Answer>()
            {
                new Answer() { Id = 1, Value="1",Questions=listQuestionAnswer1 },
                new Answer() { Id = 2, Value="2",Questions=listQuestionAnswer2 },
                new Answer() { Id = 3, Value="3",Questions=listQuestionAnswer3  },
                new Answer() { Id = 4, Value="4",Questions=listQuestionAnswer2  },
            };
            var listQuestions = new List<Question>()
            {
                new Question() { Id=1},
                new Question() { Id=2},
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Answer>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Answer>())).Callback(
                (Answer answer) => list.Add(answer));


            var service = new AnswersService(mockRepo.Object);

            var expected = new List<AnswerViewModel>()
            {
                new AnswerViewModel() { Id=1,Value="1"},
                new AnswerViewModel() { Id=4,Value="4"},
            };

            var actual = service.GetQuestionAnswersForTakesById(2);

            Assert.Equal(string.Join(" ", expected), string.Join(" ", actual));
        }
    }




    public class AnswerInTestModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }

}
