namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using SchoolQuizzes.Web.ViewModels.Shared;
    using System.Collections.Generic;

    public class IndexQuizViewModel : PagingViewModel
    {
        public IndexQuizViewModel()
        {
            this.Quizzes = new HashSet<DetailsQuizViewModel>();
        }

        public ICollection<DetailsQuizViewModel> Quizzes { get; set; }
    }
}
