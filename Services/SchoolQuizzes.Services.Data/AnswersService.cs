namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Answers;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public AnswersService(IDeletableEntityRepository<Answer> answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        public string GetAnswerValueById(int answerId)
        {
            return this.answerRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == answerId).Value;
        }

        public ICollection<Answer> GetQuestionAnswersById(int questionId)
        {
            return this.answerRepository
                .AllAsNoTracking()
                .Where(x => x.Questions.Any(q => q.QuestionId == questionId))
                .ToList();
        }

        public ICollection<AnswerQuizViewModel> GetQuestionAnswersFoTakesById(int questionId)
        {
            return this.answerRepository
                    .AllAsNoTracking()
                    .Where(x => x.Questions.Any(q => q.QuestionId == questionId))
                    .Select(x => new AnswerQuizViewModel { Id = x.Id, Value = x.Value })
                    .ToList();
        }
    }
}
