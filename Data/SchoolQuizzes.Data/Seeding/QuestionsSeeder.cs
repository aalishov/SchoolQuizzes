using SchoolQuizzes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolQuizzes.Data.Seeding
{
    public class QuestionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Questions.Any())
            {
                return;
            }

            string addedByUserId = dbContext.Users.FirstOrDefault().Id;
            int difficultLevelId = dbContext.DifficultLevels.FirstOrDefault(x => x.DifficultLevel == "Лесно").Id;
            int cattegoryId = dbContext.Categories.FirstOrDefault(x => x.Name == "Математика").Id;

            List<Question> questions = new List<Question>();

            for (int i = 0; i < 10; i++)
            {
                int sum = 2 + i;

                Question question = new Question()
                {
                    QuestionValue = $"Колко е {2}+{i}?",
                    AddedByUserId = addedByUserId,
                    CategoryId = cattegoryId,
                    DifficultId = difficultLevelId,
                };
                for (int j = 0; j < 4; j++)
                {
                    int answerValue = sum + (i * 2);
                    question.Answers.Add(new QuestionAnswer
                    {
                        Answer = new Answer()
                        {
                            AddedByUserId = addedByUserId,
                            AnswerValue = $"{answerValue}",
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
