namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class Quiz : BaseDeletableModel<int>
    {
        public Quiz()
        {
            this.Questions = new HashSet<QuizzesQuestions>();
        }

        [Required]
        [MaxLength(254)]
        public string Title { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(Difficult))]
        public int DifficultId { get; set; }

        public virtual Difficult Difficult { get; set; }

        public virtual ICollection<QuizzesQuestions> Questions { get; set; }
    }
}
