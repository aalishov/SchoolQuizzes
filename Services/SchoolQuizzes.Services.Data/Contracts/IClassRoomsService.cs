namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClassRoomsService
    {
        public Task AddStudentToClassRoomAsync(int roomId, int studentId);

        public Task AssignClassRoomQuizAsync(string title,int classRoomId, int quizId, bool isExam);

        public Task CreateClassRoomAsync(string userTeacherId, int stageId, int categoryId);

        public T GetRoomById<T>(int id);

        public ICollection<T> GetRooms<T>(string userId);

        public int GetRoomStageId(int roomId);

        public T GetQuizTakesDetails<T>(int classRoomQuizId);
    }
}
