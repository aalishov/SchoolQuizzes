namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using System.Collections.Generic;

    using SchoolQuizzes.Web.ViewModels.Answers;
    using SchoolQuizzes.Web.ViewModels.Shared;

    public class TakeQuestionAnswerViewModel : PagingViewModel
    {
        public TakeQuestionAnswerViewModel()
        {
            this.Answers = new List<AnswerQuizViewModel>();
        }
        public int TakeId { get; set; }

        public int QuizId { get; set; }

        public int CurrentQuestionId { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public int QuestionsCount { get; set; }

        public ICollection<AnswerQuizViewModel> Answers { get; set; }

        public int UserAnswerId { get; set; }

        public int? TakenAnswer { get; set; }
    }
}
