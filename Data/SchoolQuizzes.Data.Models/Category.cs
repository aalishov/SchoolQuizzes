namespace SchoolQuizzes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SchoolQuizzes.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Questions = new HashSet<Question>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(254)]
        public string Description { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
