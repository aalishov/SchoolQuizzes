namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class UserTakeViewModel : IMapFrom<Take>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string QuizTitle { get; set; }

        public string QuizCategoryName { get; set; }

        public string QuizDifficultName { get; set; }

        public int QuizQuestionsCount { get; set; }

        public string Result { get; set; }

    }
}
