namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Questions = new HashSet<Question>();
            this.Quizzes = new HashSet<Quiz>();
        }


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(254)]
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Quiz> Quizzes { get; set; }
    }
}
