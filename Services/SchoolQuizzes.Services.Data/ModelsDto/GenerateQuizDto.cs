namespace SchoolQuizzes.Services.Data.ModelsDto
{
    using System.Collections.Generic;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class GenerateQuizDto : IMapTo<Quiz>
    {
        public GenerateQuizDto()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Title { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public int DifficultId { get; set; }

        public int Count { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
