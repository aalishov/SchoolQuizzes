namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class IndexClassRoom : IMapFrom<ClassRoom>
    {
        public int Id { get; set; }

        public string StageName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string TeacherApplicationUserUserName { get; set; }

        public int StudentsCount { get; set; }

       
    }
}
