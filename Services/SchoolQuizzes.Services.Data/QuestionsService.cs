namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Models;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionsRepository, IDeletableEntityRepository<Answer> answerRepository)
        {
            this.questionsRepository = questionsRepository;
            this.answerRepository = answerRepository;
        }

        public async Task CreateAsync(CreateQuestionDto input)
        {
            Question question = new Question();
            question.QuestionValue = input.QuestionValue;
            question.DifficultId = input.DifficultId;
            question.CategoryId = input.CategoryId;
            question.Description = input.Description;
            question.AddedByUserId = input.AddedByUserId;

            foreach (var inputAnswer in input.Answers)
            {
                var answer = this.answerRepository.All().FirstOrDefault(a => a.AnswerValue == inputAnswer.AnswerValue);
                bool isCorrect = inputAnswer.IsTrue == "on" ? true : false;
                if (answer == null)
                {
                    answer = new Answer()
                    {
                        AnswerValue = inputAnswer.AnswerValue,
                        Description = inputAnswer.Description,
                        AddedByUserId = input.AddedByUserId,
                    };
                }

                question.Answers.Add(new QuestionAnswer { Answer = answer, IsCorrect = isCorrect });
            }

            await this.questionsRepository.AddAsync(question);
            await this.questionsRepository.SaveChangesAsync();
        }

        public ICollection<Question> GetQuestionsForQuiz(int categoryId, int difficultId, int count)
        {
            return this.questionsRepository.AllAsNoTracking()
                .Where(q => q.CategoryId == categoryId && q.DifficultId == difficultId)
                .Take(count)
                .ToList();
        }
    }
}
