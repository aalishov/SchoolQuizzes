namespace SchoolQuizzes.Web.ViewModels.Answers
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.ComponentModel.DataAnnotations;

    public class CreateAnswerViewModel : IMapTo<Answer>
    {
        [Required]
        [MinLength(1)]
        public string AnswerValue { get; set; }


        public string IsTrue { get; set; }
    }
}
