namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;

    public class QuestionsSeeder : ISeeder
    {
        private const int QuestionsCount = 50;

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

            string addedByUserId = dbContext.Users.FirstOrDefault().Id;
            int difficultLevelId = dbContext.DifficultLevels.FirstOrDefault(x => x.Name == "Лесно").Id;
            int cattegoryId = dbContext.Categories.FirstOrDefault(x => x.Name == "Математика").Id;

            List<Question> questions = new List<Question>();
            Random random = new Random();
            Stage stage = dbContext.Stages.FirstOrDefault();

            for (int i = 0; i < QuestionsCount; i++)
            {
                int randomNumber = random.Next(-8, 8);
                int sum = randomNumber + i + i;

                Question question = new Question()
                {
                    Value = $"Колко е {randomNumber + i}+{i}?",
                    AddedByUserId = addedByUserId,
                    CategoryId = cattegoryId,
                    DifficultId = difficultLevelId,
                    Stage = stage,
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
