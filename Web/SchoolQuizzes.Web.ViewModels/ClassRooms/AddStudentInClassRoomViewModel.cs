namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using AutoMapper.Configuration.Annotations;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class AddStudentInClassRoomViewModel : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string ApplicationUserUserName { get; set; }
    }
}
