namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public interface IQuestionsService
    {
        Task CreateAsync(CreateQuestionViewModel questionDto);
        ICollection<Question> GetQuestionsForQuiz(int categoryId, int difficultId, int count);

        ICollection<Question> GetQuestionsByQuizId(int quizId);
    }
}
