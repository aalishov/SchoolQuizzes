﻿namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        public Task CreateAsync(CreateQuestionViewModel questionDto);

        public ICollection<Question> GetQuestionsForQuiz(int categoryId, int difficultId, int count);

        public ICollection<Question> GetRandomQuestionsForQuiz(int categoryId, int difficultId, int count);

        public ICollection<T> GetQuestionsByQuizId<T>(int quizId);

        public string GetQuestionValueById(int questionId);

        public double GetQuestionRatingById(int questionId);

        public bool IsCorrectAnswer(int questionId, int asnwerId);
    }
}
