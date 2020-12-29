namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;

    public class QuestionsSeeder : ISeeder
    {
        private const int QuestionsCount = 500;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedMathQuestionsAsync(dbContext);
        }

        private static async Task SeedMathQuestionsAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Questions.Any())
            {
                return;
            }

            Category category = dbContext.Categories.FirstOrDefault(x => x.Name == "Математика");
            List<Question> questions = new List<Question>();
            Random random = new Random();

            for (int i = 0; i < QuestionsCount; i++)
            {
                int randomNumber = random.Next(-8, 8);
                int sum = randomNumber + i + i;

                Question question = new Question()
                {
                    Value = $"Колко е {randomNumber + i}+{i}?",
                    AddedByUser = dbContext.Teachers.OrderBy(x => Guid.NewGuid()).FirstOrDefault().ApplicationUser,
                    Category = category,
                    Difficult = dbContext.DifficultLevels.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    Stage = dbContext.Stages.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                };
                for (int j = 0; j < 4; j++)
                {
                    int answerValue = sum + (j * 2);
                    question.Answers.Add(new QuestionAnswer
                    {
                        Answer = new Answer()
                        {
                            Value = $"{answerValue}",
                        },
                        IsCorrect = sum == answerValue,
                    });
                }

                questions.Add(question);
            }

            await dbContext.Questions.AddRangeAsync(questions);
            _ = await dbContext.SaveChangesAsync();
        }
    }
}
