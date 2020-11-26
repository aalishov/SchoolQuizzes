namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SchoolQuizzes.Data.Models;

    public interface IAnswersService
    {
        ICollection<Answer> GetQuestionAnswersById(int questionId);
    }
}
