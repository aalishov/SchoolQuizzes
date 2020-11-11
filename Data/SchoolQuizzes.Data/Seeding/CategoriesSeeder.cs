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

            await dbContext.Categories.AddAsync(new Category() { Name = "Математика", Description = "Въпроси от категория математика" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Информатика", Description = "Въпроси от категория информатика" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Информационни технологии" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Български език и литература" });
            await dbContext.Categories.AddAsync(new Category() { Name = "История" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Георгафия" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Химия" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Биология" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Физика" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Изобразително изкуство" });
            await dbContext.Categories.AddAsync(new Category() { Name = "Музика" });

            await dbContext.SaveChangesAsync();
        }
    }
}
