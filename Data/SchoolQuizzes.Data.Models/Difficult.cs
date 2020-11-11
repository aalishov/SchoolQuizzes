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
        }

        // easy, medium, or difficult
        [Required]
        [MaxLength(30)]
        public string DifficultLevel { get; set; }

        [MaxLength(127)]
        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
