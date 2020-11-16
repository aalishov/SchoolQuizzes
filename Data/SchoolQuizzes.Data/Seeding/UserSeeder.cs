﻿using Microsoft.AspNetCore.Identity;
using SchoolQuizzes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolQuizzes.Data.Seeding
{
    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }
            await dbContext.Users.AddAsync(new ApplicationUser()
            {
                UserName = "alishov",
                Email = "aalishov@live.com",
                //passwprdHash = passowrd - 123456
                PasswordHash = "123456"
            });
        }
    }
}
