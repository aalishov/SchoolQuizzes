namespace SchoolQuizzes.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeachersAndStudentsSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {


            if (dbContext.Teachers.Any() && dbContext.Students.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            

            foreach (var user in dbContext.Users)
            {
                if (await userManager.IsInRoleAsync(user, GlobalConstants.TeacherRoleName))
                {
                    _ = dbContext.Teachers.Add(new Teacher() { ApplicationUser = user });
                }
                else if (await userManager.IsInRoleAsync(user, GlobalConstants.StudentRoleName))
                {
                    Stage stage = dbContext.Stages.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    _ = dbContext.Students.Add(new Student() { ApplicationUser = user, Stage = stage });
                }
            }
            _ = await dbContext.SaveChangesAsync();
        }
    }
}
