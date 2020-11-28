namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class QuestionAnswer
    {
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [ForeignKey(nameof(Answer))]
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
