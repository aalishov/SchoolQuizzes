namespace SchoolQuizzes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SchoolQuizzes.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.Questions = new HashSet<QuestionAnswer>();
        }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [Required]
        [MaxLength(254)]
        public string Value { get; set; }

        public virtual ICollection<QuestionAnswer> Questions { get; set; }
    }
}
