namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.Collections.Generic;

    public class DetailsClassRoomViewModel : IMapFrom<ClassRoom>
    {
        public DetailsClassRoomViewModel()
        {
            this.Students = new List<StudentInClassRoomViewModel>();
            this.ClassRoomQuizzes = new List<ClassRoomQuizListViewModel>();
        }

        public int Id { get; set; }

        public string StageName { get; set; }

        public string CategoryName { get; set; }

        public string TeacherApplicationUserUserName { get; set; }

        public int StudentsCount { get; set; }

        public ICollection<StudentInClassRoomViewModel> Students { get; set; }

        public ICollection<ClassRoomQuizListViewModel> ClassRoomQuizzes { get; set; }
    }
}
