namespace SchoolQuizzes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SchoolQuizzes.Data.Common.Models;

    [Table("DifficultLevels")]
    public class Difficult : BaseDeletableModel<int>
    {
        public Difficult()
        {
            this.Questions = new HashSet<Question>();
            this.Quizzes = new HashSet<Quiz>();
        }

        // easy, medium, or difficult
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(127)]
        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
