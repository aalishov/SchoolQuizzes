namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Models;

    public class UserSeeder : ISeeder
    {
        private const string School = "СУ \"Васил Левски\"";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
             await SeedUserAsync(dbContext, userManager, roleManager, "Admin", "Admin", "admin@abv.bg", "admin@abv.bg", configuration["Password:AdminPassword"], GlobalConstants.AdministratorRoleName);
            await SeedUserAsync(dbContext, userManager, roleManager, "Alish", "Alishov", "aalishov@live.com", "aalishov@live.com", configuration["Password:AdminPassword"], GlobalConstants.AdministratorRoleName);

            for (int i = 0; i < 25; i++)
            {
                await SeedUserAsync(dbContext, userManager, roleManager, $"TName{i}", $"TLastName{i}", $"teacher{i}@live.com", $"teacher{i}@live.com", configuration["Password:DefaultPassword"], GlobalConstants.TeacherRoleName);
            }

            for (int i = 0; i < 200; i++)
            {
                await SeedUserAsync(dbContext, userManager, roleManager, $"SName{i}", $"SLastName{i}", $"student{i}@live.com", $"student{i}@live.com", configuration["Password:DefaultPassword"], GlobalConstants.StudentRoleName);
            }
        }

        private static async Task SeedUserAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string firstName, string lastName, string userName, string email, string password, string roleName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                IdentityResult result = await userManager.CreateAsync(
                    new ApplicationUser()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        School = School,
                        UserName = userName,
                        Email = email,
                    }, password);


                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            user = await userManager.FindByNameAsync(userName);

            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                var result = await userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
