namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;

    public class QuizzesSeeder : ISeeder
    {
        private const int QuestionsCount = 7;
        private const int QuizzesCount = 10;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await AddQuizzesAsync(dbContext);
        }

        private static async Task AddQuizzesAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Quizzes.Any())
            {
                return;
            }

            string addedByUserId = dbContext.Users.FirstOrDefault().Id;
            int difficultLevelId = dbContext.DifficultLevels.FirstOrDefault(x => x.Name == "Лесно").Id;
            int cattegoryId = dbContext.Categories.FirstOrDefault(x => x.Name == "Математика").Id;
            for (int i = 0; i < QuizzesCount; i++)
            {
                Quiz quiz = new Quiz
                {
                    Title = $"Тест №{i}",
                    CategoryId = cattegoryId,
                    DifficultId = difficultLevelId,
                    AddedByUserId = addedByUserId,
                };

                List<Question> questions = dbContext.Questions.OrderBy(x => Guid.NewGuid()).Take(QuestionsCount).ToList();

                foreach (var question in questions)
                {
                    quiz.Questions.Add(new QuizQuestion { QuestionId = question.Id });
                }

                _ = await dbContext.Quizzes.AddAsync(quiz);
            }

            _ = await dbContext.SaveChangesAsync();
        }
    }
}
