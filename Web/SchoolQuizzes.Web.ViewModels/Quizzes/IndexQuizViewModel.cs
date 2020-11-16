namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexQuizViewModel
    {
        public IndexQuizViewModel()
        {
            this.Quizzes = new HashSet<DetailsQuizViewModel>();
        }
        public ICollection<DetailsQuizViewModel> Quizzes { get; set; }
    }
}
