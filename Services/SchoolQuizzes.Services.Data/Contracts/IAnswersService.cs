namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Answers;

    public interface IAnswersService
    {
        ICollection<Answer> GetQuestionAnswersById(int questionId);
        ICollection<AnswerQuizViewModel> GetQuestionAnswersFoTakesById(int questionId);
    }
}
