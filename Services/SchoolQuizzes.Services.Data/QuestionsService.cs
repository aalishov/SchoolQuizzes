namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionsRepository, IDeletableEntityRepository<Answer> answerRepository)
        {
            this.questionsRepository = questionsRepository;
            this.answerRepository = answerRepository;
        }

        public async Task CreateAsync(CreateQuestionViewModel input)
        {
            Question question = new Question();
            question.Value = input.QuestionValue;
            question.DifficultId = input.DifficultId;
            question.CategoryId = input.CategoryId;
            question.Description = input.Description;
            question.AddedByUserId = input.UserId;

            foreach (var inputAnswer in input.Answers)
            {
                var answer = this.answerRepository.All().FirstOrDefault(a => a.Value == inputAnswer.AnswerValue);
                bool isCorrect = inputAnswer.IsTrue == "on" ? true : false;
                if (answer == null)
                {
                    answer = new Answer()
                    {
                        Value = inputAnswer.AnswerValue,
                        AddedByUserId = input.UserId,
                    };
                }

                question.Answers.Add(new QuestionAnswer { Answer = answer, IsCorrect = isCorrect });
            }

            await this.questionsRepository.AddAsync(question);
            await this.questionsRepository.SaveChangesAsync();
        }

        public ICollection<Question> GetQuestionsByQuizId(int quizId)
        {
            return this.questionsRepository
                .AllAsNoTracking()
                .Where(x => x.Quizzes.Any(q => q.QuizId == quizId))
                .ToList();
        }

        public ICollection<Question> GetQuestionsForQuiz(int categoryId, int difficultId, int count)
        {
            return this.questionsRepository.AllAsNoTracking()
                .Where(q => q.CategoryId == categoryId && q.DifficultId == difficultId)
                .Take(count)
                .ToList();
        }

        public ICollection<Question> GetRandomQuestionsForQuiz(int categoryId, int difficultId, int count)
        {
            ICollection<Question> questions = new List<Question>();
            List<int> questionsId = this.questionsRepository.AllAsNoTracking()
                .Where(x => x.CategoryId == categoryId && x.DifficultId == difficultId)
                .Select(x => x.Id)
                 .ToList();

            for (int i = 0; i < count; i++)
            {
                Random random = new Random();
                int randIndex = random.Next(0, questionsId.Count - 1);
                int questionIndex = questionsId[randIndex];
                questionsId.RemoveAt(randIndex);
                questions.Add(this.questionsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == questionIndex));
            }

            return questions;
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


    }
}
