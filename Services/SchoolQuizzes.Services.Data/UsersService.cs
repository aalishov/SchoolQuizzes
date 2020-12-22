namespace SchoolQuizzes.Services.Data
{
    using AutoMapper;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Users;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Teacher> teachersRepository;
        private readonly IDeletableEntityRepository<Student> studentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IMapper mapper;

        public UsersService(IDeletableEntityRepository<Teacher> teachersRepository, IDeletableEntityRepository<Student> studentsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.teachersRepository = teachersRepository;
            this.studentsRepository = studentsRepository;
            this.usersRepository = usersRepository;
            this.mapper = AutoMapperConfig.MapperInstance;
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


        public T GetStudentByUserId<T>(string userId)
        {
            return this.studentsRepository.AllAsNoTracking()
                 .Where(x => x.ApplicationUserId == userId)
                 .To<T>()
                 .FirstOrDefault();
        }

        public async Task UpdateStudentByUserIdAsync(EditStudentViewModel updatedUser)
        {
            Student student = this.studentsRepository.All().FirstOrDefault(x => x.Id == updatedUser.Student.Id);

            ApplicationUser user = this.usersRepository.All().FirstOrDefault(x => x.Id == updatedUser.ApplicationUser.Id);
            if (student != null && user!=null)
            {
                this.mapper.Map<BaseStudentVM, Student>(updatedUser.Student, student);
                this.mapper.Map<BaseApplicationUserVM, ApplicationUser>(updatedUser.ApplicationUser, user);
                this.studentsRepository.Update(student);
                this.usersRepository.Update(user);
                await this.studentsRepository.SaveChangesAsync();
                await this.usersRepository.SaveChangesAsync();
            }
        }
    }
}
