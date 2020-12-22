namespace SchoolQuizzes.Web.ViewModels.Users
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class BaseTeacherVM : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
