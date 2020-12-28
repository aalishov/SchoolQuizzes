namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IMapper mapper;

        public QuestionsService(IDeletableEntityRepository<Question> questionsRepository, IDeletableEntityRepository<Answer> answerRepository)
        {
            this.questionsRepository = questionsRepository;
            this.answerRepository = answerRepository;
            this.mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task CreateAsync(CreateQuestionViewModel input)
        {
            Question question = new Question
            {
                Value = input.QuestionValue,
                DifficultId = input.DifficultId,
                CategoryId = input.CategoryId,
                StageId = input.StageId,
                Description = input.Description,
                AddedByUserId = input.UserId,
            };

            foreach (var inputAnswer in input.Answers)
            {
                var answer = this.answerRepository.All().FirstOrDefault(a => a.Value == inputAnswer.AnswerValue);
                bool isCorrect = inputAnswer.IsTrue == "on";
                if (answer == null)
                {
                    answer = new Answer()
                    {
                        Value = inputAnswer.AnswerValue,
                    };
                }

                question.Answers.Add(new QuestionAnswer { Answer = answer, IsCorrect = isCorrect });
            }

            await this.questionsRepository.AddAsync(question);
            await this.questionsRepository.SaveChangesAsync();
        }

        public ICollection<T> GetQuestionsByQuizId<T>(int quizId)
        {
            return this.questionsRepository
                .AllAsNoTracking()
                .Where(x => x.Quizzes.Any(q => q.QuizId == quizId))
                .To<T>()
                .ToList();
        }

        public ICollection<Question> GetQuestionsForQuiz(int categoryId, int difficultId, int count)
        {
            return this.questionsRepository.AllAsNoTracking()
                .Where(q => q.CategoryId == categoryId && q.DifficultId == difficultId)
                .Take(count)
                .ToList();
        }

        public ICollection<Question> GetRandomQuestionsForQuiz(int categoryId, int difficultId, int stageId, int count)
        {
            return this.questionsRepository.AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.CategoryId == categoryId && x.DifficultId == difficultId && x.StageId == stageId)
                .Take(count)
                .ToList();
        }

        public string GetQuestionValueById(int questionId)
        {
            return this.questionsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == questionId).Value;
        }

        public bool IsCorrectAnswer(int questionId, int asnwerId)
        {
            var answers = this.questionsRepository.AllAsNoTracking()
                 .Select(x => new
                 {
                     x.Id,
                     Answers = x.Answers
                        .Select(a =>
                        new
                        {
                            a.AnswerId,
                            a.IsCorrect,
                        }),
                 })
                 .FirstOrDefault(x => x.Id == questionId)
                 .Answers
                 .ToList();
            bool isCorrect = answers.FirstOrDefault(x => x.AnswerId == asnwerId).IsCorrect;
            return isCorrect;
        }

        public double GetQuestionRatingById(int questionId)
        {
            return this.questionsRepository.All()
                .Select(x => new { x.Id, Ratings = x.Ratings.ToList() })
                .FirstOrDefault(x => x.Id == questionId)
                .Ratings.Count() == 0 ? 0 : this.questionsRepository.All()
                .Select(x => new { x.Id, Ratings = x.Ratings.ToList() })
                .FirstOrDefault(x => x.Id == questionId)
                .Ratings.Average(x => x.Value);
        }
    }
}
