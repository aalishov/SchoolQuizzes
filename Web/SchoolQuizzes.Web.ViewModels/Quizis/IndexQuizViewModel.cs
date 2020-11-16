using SchoolQuizzes.Data.Models;
using System.Collections.Generic;

namespace SchoolQuizzes.Web.ViewModels.Quizis
{
    public class IndexQuizViewModel
    {
        public ICollection<Quiz> Quizzes { get; set; }

        public int QuestionCount { get; set; }
    }
}
