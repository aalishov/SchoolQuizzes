namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        public Task AddTeacher(ApplicationUser user);

        public Task AddStudent(ApplicationUser user);

        public T GetStudentByUserId<T>(string userId);

        public T GetStudentById<T>(int id);

        ICollection<T> GetAllStudentsInClassRoom<T>(int classRoomId);

        public T GetTeacherByUserId<T>(string userId);

        public T GetTeacherById<T>(int id);

        public ICollection<T> GetAllStudentsByStageId<T>(int roomId, int stageId);

        public Task UpdateStudentByUserIdAsync(EditStudentViewModel user);
    }
}
