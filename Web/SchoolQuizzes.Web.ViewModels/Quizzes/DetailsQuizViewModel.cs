namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public class DetailsQuizViewModel
    {
        public DetailsQuizViewModel()
        {
            this.Questions = new List<QuestionQuizViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Трудност: ")]
        public string Difficult { get; set; }

        [Display(Name ="Категория: ")]
        public string Category { get; set; }

        public ICollection<QuestionQuizViewModel> Questions { get; set; }
    }
}
