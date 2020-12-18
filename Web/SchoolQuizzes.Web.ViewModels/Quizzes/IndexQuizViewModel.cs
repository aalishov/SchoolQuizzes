namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;

    public class IndexQuizViewModel
    {
        public IndexQuizViewModel()
        {
            this.Quizzes = new HashSet<DetailsQuizViewModel>();
        }
        public ICollection<DetailsQuizViewModel> Quizzes { get; set; }
    }
}
