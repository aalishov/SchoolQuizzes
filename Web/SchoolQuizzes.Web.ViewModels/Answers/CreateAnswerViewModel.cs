namespace SchoolQuizzes.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnswerViewModel
    {
        [Required]
        [MinLength(1)]
        public string AnswerValue { get; set; }


        public string IsTrue { get; set; }
    }
}
