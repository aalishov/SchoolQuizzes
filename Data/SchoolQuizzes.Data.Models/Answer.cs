﻿namespace SchoolQuizzes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SchoolQuizzes.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.Questions = new HashSet<QuestionAnswer>();
        }

        [Required]
        [MaxLength(254)]
        public string Value { get; set; }

        public virtual ICollection<QuestionAnswer> Questions { get; set; }
    }
}
