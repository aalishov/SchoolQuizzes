namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SchoolQuizzes.Web.ViewModels.Shared;

    public class TakeQuestionAnswerViewModel : PagingViewModel
    {
        public TakeQuestionAnswerViewModel()
        {
            this.Answers = new List<string>();
        }

        public int QuizId { get; set; }

        public int CurrentQuestionId { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public ICollection<string> Answers { get; set; }

        public string UserAnswer { get; set; }
    }
}
