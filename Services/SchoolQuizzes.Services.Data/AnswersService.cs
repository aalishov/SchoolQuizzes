namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public AnswersService(IDeletableEntityRepository<Answer> answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        public ICollection<Answer> GetQuestionAnswersById(int questionId)
        {
            return this.answerRepository
                .AllAsNoTracking()
                .Where(x => x.Questions.Any(q => q.QuestionId == questionId))
                .ToList();
        }
    }
}
