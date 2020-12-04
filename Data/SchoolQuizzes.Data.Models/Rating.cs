namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;

    public class Rating : BaseModel<int>
    {
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Value { get; set; }
    }
}
