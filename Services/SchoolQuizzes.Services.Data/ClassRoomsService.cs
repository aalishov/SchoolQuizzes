namespace SchoolQuizzes.Services.Data
{
    using System;

    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SchoolQuizzes.Web.ViewModels.Users;

    public class ClassRoomsService : IClassRoomsService
    {
        private readonly IDeletableEntityRepository<ClassRoom> classRoom;
        private readonly IRepository<ClassRoomStudent> roomStudentRepository;
        private readonly IRepository<ClassRoomQuiz> classRoomQuizRepository;
        private readonly IUsersService usersService;

        public ClassRoomsService(IDeletableEntityRepository<ClassRoom> classRoom, IRepository<ClassRoomStudent> roomStudentRepository, IRepository<ClassRoomQuiz> classRoomQuizRepository, IUsersService usersService)
        {
            this.classRoom = classRoom;
            this.roomStudentRepository = roomStudentRepository;
            this.classRoomQuizRepository = classRoomQuizRepository;
            this.usersService = usersService;
        }

        public async Task AddStudentToClassRoomAsync(int roomId, int studentId)
        {
            ClassRoom classRoom = this.classRoom.All().FirstOrDefault(x => x.Id == roomId);
            await this.roomStudentRepository.AddAsync(new ClassRoomStudent() { ClassRoom = classRoom, StudentId = studentId });
            await this.roomStudentRepository.SaveChangesAsync();
        }

        public async Task CreateClassRoomAsync(string userTeacherId, int stageId, int categoryId)
        {
            var teacher = this.usersService.GetTeacherByUserId<BaseTeacherVM>(userTeacherId);

            var room = new ClassRoom()
            {
                ClassRoomCode = Guid.NewGuid().ToString(),
                TeacherId = teacher.Id,
                StageId = stageId,
                CategoryId = categoryId,
            };

            await this.classRoom.AddAsync(room);
            await this.classRoom.SaveChangesAsync();
        }

        public ICollection<T> GetRooms<T>(string userId)
        {
            return this.classRoom.AllAsNoTracking()
                .Where(x => x.Teacher.ApplicationUserId == userId || x.Students.Any(x => x.Student.ApplicationUserId == userId))
                .To<T>()
                .ToList();
        }

        public T GetRoomById<T>(int id)
        {
            return this.classRoom.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetRoomStageId(int roomId)
        {
            return this.classRoom.AllAsNoTracking().FirstOrDefault(x => x.Id == roomId).StageId;
        }

        public async Task AssignClassRoomQuizAsync(string title, int classRoomId, int quizId, bool isExam)
        {
            await this.classRoomQuizRepository.AddAsync(new ClassRoomQuiz { Title = title, QuizId = quizId, ClassRoomId = classRoomId, IsExam = isExam });
            await this.classRoomQuizRepository.SaveChangesAsync();
        }
    }
}
