namespace SchoolQuizzes.Web.ViewModels.Answers
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
