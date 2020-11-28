using SchoolQuizzes.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolQuizzes.Data.Models
{
    public class Take : BaseDeletableModel<int>
    {
        public Take()
        {
            this.TakedAnswers = new HashSet<TakedAnswer>();
        }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public bool IsFinished { get; set; }

        public virtual ICollection<TakedAnswer> TakedAnswers { get; set; }
    }
}
