namespace SchoolQuizzes.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateAnswerViewModel
    {
        [Required]
        [MinLength(1)]
        public string AnswerValue { get; set; }


        public string IsTrue { get; set; }
    }
}
