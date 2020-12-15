namespace SchoolQuizzes.Services.Data
{
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Teacher> teachersRepository;
        private readonly IDeletableEntityRepository<Student> studentsRepository;

        public UsersService(IDeletableEntityRepository<Teacher> teachersRepository, IDeletableEntityRepository<Student> studentsRepository)
        {
            this.teachersRepository = teachersRepository;
            this.studentsRepository = studentsRepository;
        }

        public async Task AddStudent(ApplicationUser user)
        {
            await this.studentsRepository.AddAsync(new Student { ApplicationUser = user });
            await this.studentsRepository.SaveChangesAsync();
        }

        public async Task AddTeacher(ApplicationUser user)
        {
            await this.teachersRepository.AddAsync(new Teacher { ApplicationUser = user });
            await this.teachersRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAllStudentsByStageId<T>(int roomId, int stageId)
        {
            return this.studentsRepository.AllAsNoTracking()
                .Where(x => x.StageId == stageId && !x.ClassRooms.Any(x => x.ClassRoomId == roomId))
                .To<T>()
                .ToList();
        }


    }
}
