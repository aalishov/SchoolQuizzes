namespace SchoolQuizzes.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuizQuestion
    {
        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
