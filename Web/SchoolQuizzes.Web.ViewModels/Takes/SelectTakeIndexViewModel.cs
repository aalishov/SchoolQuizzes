namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SelectTakeIndexViewModel
    {
        [Display(Name = "Тестове")]
        public int QuizId { get; set; }

        public ICollection<KeyValuePair<string, string>> QuizzesItems { get; set; }
    }
}
