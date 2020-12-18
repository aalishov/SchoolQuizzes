﻿namespace SchoolQuizzes.Data.Seeding
{
    using SchoolQuizzes.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class StageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Stages.Any())
            {
                return;
            }

            _ = await dbContext.Stages.AddAsync(new Stage() { Name = "Забавни", Description = "Ученици" });

            for (int i = 1; i <= 12; i++)
            {
                _ = await dbContext.Stages.AddAsync(new Stage() { Name = $"{i} клас", Description = "Ученици" });
            }

            for (int i = 1; i <= 4; i++)
            {
                _ = await dbContext.Stages.AddAsync(new Stage() { Name = $"{i} курс", Description = "Студенти" });
            }

            _ = await dbContext.SaveChangesAsync();
        }
    }
}
