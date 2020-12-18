namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class StudentInClassRoomViewModel : IMapFrom<ClassRoomStudent>
    {
        public string StudentStageName { get; set; }

        public string StudentApplicationUserUserName { get; set; }
    }
}
