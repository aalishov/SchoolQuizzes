﻿namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Quizzes;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public interface IQuizzesService
    {
        public Task CreateAsync(GenerateQuizDto questionDto);

        public string GetGuizNameById(int id);

        public ICollection<Quiz> GetQuizzes();

        public Quiz GetQuizById(int id);

        public DetailsQuizViewModel GetQuizWithQuestionsAndAnswersById(int id);
    }
}
