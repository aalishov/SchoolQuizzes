namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public Task AddTeacher(ApplicationUser user);

        public Task AddStudent(ApplicationUser user);

        public ICollection<T> GetAllStudentsByStageId<T>(int roomId, int stageId);
    }
}
