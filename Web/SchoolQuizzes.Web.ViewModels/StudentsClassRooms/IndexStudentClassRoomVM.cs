using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Mapping;

namespace SchoolQuizzes.Web.ViewModels.StudentsClassRooms
{
    public class IndexStudentClassRoomVM : IMapFrom<ClassRoom>
    {
        public int Id { get; set; }

        public string StageName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string TeacherApplicationUserUserName { get; set; }
    }
}