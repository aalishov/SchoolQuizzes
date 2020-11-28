namespace SchoolQuizzes.Services.Data.ModelsDto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SchoolQuizzes.Data.Models;

    public class GenerateQuizDto
    {
        public GenerateQuizDto()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Title { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public int DifficultId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
