namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class QuizisQuestions : BaseDeletableModel<int>
    {
        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }

        public virtual Quiz Answer { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }


    }
}
