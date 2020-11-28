namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class TakedAnswer
    {
        [Required]
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required]
        [ForeignKey(nameof(Answer))]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        [Required]
        [ForeignKey(nameof(Take))]
        public int TakeId { get; set; }

        public Take Take { get; set; }
    }
}
