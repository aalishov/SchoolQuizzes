namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClassRoomsService
    {
        public Task CreateClassRoomAsync(string userTeacherId, int stageId, int categoryId);

        public Task AddStudentToClassRoomAsync(int roomId, int studentId);

        public ICollection<T> GetRooms<T>(string userId);

        public T GerRoomById<T>(int id);

        public int GetRoomStageId(int roomId);
    }
}
