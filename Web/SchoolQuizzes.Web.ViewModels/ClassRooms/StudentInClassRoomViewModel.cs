namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class StudentInClassRoomViewModel : IMapFrom<Student>
    {
        public string StageName { get; set; }

        public string ApplicationUserUserName { get; set; }
    }
}
