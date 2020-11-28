namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using SchoolQuizzes.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            string addedByUserId = dbContext.Users.FirstOrDefault().Id;
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Математика", Description = "Въпроси от категория математика" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Информатика", Description = "Въпроси от категория информатика" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Информационни технологии" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Български език и литература" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "История" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Георгафия" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Химия" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Биология" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Физика" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Изобразително изкуство" });
            await dbContext.Categories.AddAsync(new Category() { AddedByUserId = addedByUserId, Name = "Музика" });

            await dbContext.SaveChangesAsync();
        }
    }
}
