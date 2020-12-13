﻿namespace SchoolQuizzes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SchoolQuizzes.Data.Common.Models;

    public class Question : BaseDeletableModel<int>
    {
        public Question()
        {
            this.Answers = new HashSet<QuestionAnswer>();
            this.Quizzes = new HashSet<QuizzesQuestions>();
            this.Ratings = new HashSet<Rating>();
        }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [Required]
        [MaxLength(254)]
        public string Value { get; set; }

        [MaxLength(254)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(Difficult))]
        public int DifficultId { get; set; }

        public virtual Difficult Difficult { get; set; }

        [ForeignKey(nameof(Stage))]
        public int StageId { get; set; }

        public virtual Stage Stage { get; set; }

        public virtual ICollection<QuestionAnswer> Answers { get; set; }

        public virtual ICollection<QuizzesQuestions> Quizzes { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
