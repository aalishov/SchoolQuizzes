namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Answers;

    public interface IAnswersService
    {
        ICollection<T> GetQuestionAnswersById<T>(int questionId);

        ICollection<AnswerQuizViewModel> GetQuestionAnswersForTakesById(int questionId);

        public string GetAnswerValueById(int answerId);
    }
}
