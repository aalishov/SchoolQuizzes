namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public Task AddTeacher(ApplicationUser user);

        public Task AddStudent(ApplicationUser user);
    }
}
