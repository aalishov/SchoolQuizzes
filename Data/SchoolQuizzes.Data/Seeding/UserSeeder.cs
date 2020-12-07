﻿namespace SchoolQuizzes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Models;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedUserAsync(userManager, roleManager, "admin@abv.bg", "admin@abv.bg", "123456", GlobalConstants.AdministratorRoleName);
            await SeedUserAsync(userManager, roleManager, "aalishov@live.com", "aalishov@live.com", "123456", GlobalConstants.AdministratorRoleName);
            await SeedUserAsync(userManager, roleManager, "teacher1@live.com", "teacher1@live.com", "123456", GlobalConstants.TeacherRoleName);
            await SeedUserAsync(userManager, roleManager, "teacher2@live.com", "teacher2@live.com", "123456", GlobalConstants.TeacherRoleName);
            await SeedUserAsync(userManager, roleManager, "teacher3@live.com", "teacher3@live.com", "123456", GlobalConstants.TeacherRoleName);
            for (int i = 0; i < 50; i++)
            {
                await SeedUserAsync(userManager, roleManager, $"student{i}@live.com", $"student{i}@live.com", "123456", GlobalConstants.StudentRoleName);
            }
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string userName, string email, string password, string roleName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser()
                    {
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
