namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SchoolQuizzes.Web.ViewModels.Answers;

    public interface IAnswersService
    {
        ICollection<T> GetQuestionAnswersById<T>(int questionId);

        ICollection<AnswerViewModel> GetQuestionAnswersForTakesById(int questionId);

        public string GetAnswerValueById(int answerId);
    }
}
