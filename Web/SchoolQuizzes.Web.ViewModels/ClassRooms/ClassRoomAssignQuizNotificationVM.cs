namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class ClassRoomAssignQuizNotificationVM : IMapFrom<ClassRoomQuiz>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ClassRoomTeacherApplicationUserFirstName { get; set; }

        public string ClassRoomTeacherApplicationUserLasttName { get; set; }

        public string ClassRoomTeacherApplicationUserEmail { get; set; }

        public bool IsExam { get; set; }
    }
}
