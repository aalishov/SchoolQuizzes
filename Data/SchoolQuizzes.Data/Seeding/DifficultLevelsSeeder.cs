namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using SchoolQuizzes.Data.Models;

    public class DifficultLevelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DifficultLevels.Any())
            {
                return;
            }

            await dbContext.DifficultLevels.AddAsync(new Difficult { Name = "Лесно", });
            await dbContext.DifficultLevels.AddAsync(new Difficult { Name = "Средно", });
            await dbContext.DifficultLevels.AddAsync(new Difficult { Name = "Трудно", });
        }
    }
}
