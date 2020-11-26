using SchoolQuizzes.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolQuizzes.Data.Models
{
    public class TakedAnswer : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required]
        [ForeignKey(nameof(Answer))]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
