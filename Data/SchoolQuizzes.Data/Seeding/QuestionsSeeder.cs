namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;

    public class QuestionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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


            for (int i = 0; i < 10; i++)
            {
                int randomNumber = random.Next(-8, 8);
                int sum = randomNumber + i + i;

                Question question = new Question()
                {
                    Value = $"Колко е {randomNumber + i}+{i}?",
                    AddedByUserId = addedByUserId,
                    CategoryId = cattegoryId,
                    DifficultId = difficultLevelId,
                };
                for (int j = 0; j < 4; j++)
                {
                    int answerValue = sum + (j * 2);
                    question.Answers.Add(new QuestionAnswer
                    {
                        Answer = new Answer()
                        {
                            AddedByUserId = addedByUserId,
                            Value = $"{answerValue}",
                        },
                        IsCorrect = sum == answerValue ? true : false,
                    });
                }

                questions.Add(question);
            }

            await dbContext.Questions.AddRangeAsync(questions);
            await dbContext.SaveChangesAsync();
        }
    }
}
