namespace SchoolQuizzes.Web.ViewModels.Answers
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class AnswerQuizViewModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
