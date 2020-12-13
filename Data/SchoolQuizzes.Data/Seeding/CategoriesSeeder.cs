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

            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Математика", Description = "Въпроси от категория математика" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Информатика", Description = "Въпроси от категория информатика" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Информационни технологии" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Български език и литература" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "История" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Георгафия" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Химия" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Биология" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Физика" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Изобразително изкуство" });
            _ = await dbContext.Categories.AddAsync(new Category() { Name = "Музика" });

            _ = await dbContext.SaveChangesAsync();
        }
    }
}
