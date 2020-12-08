namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.Collections.Generic;

    public class UserTakeDetailViewModel : IMapFrom<Take>
    {
        public UserTakeDetailViewModel()
        {
            this.AnsweredQuestions = new List<TakedQuestionViewModel>();
        }

        public string QuizTitle { get; set; }

        public string QuizCategoryName { get; set; }

        public string QuizDifficultName { get; set; }

        public ICollection<TakedQuestionViewModel> AnsweredQuestions { get; set; }
    }
}
