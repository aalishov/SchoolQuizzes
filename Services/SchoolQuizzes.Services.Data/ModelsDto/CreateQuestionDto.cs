namespace SchoolQuizzes.Services.Data.ModelsDto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateQuestionDto
    {
        public CreateQuestionDto()
        {
            this.Answers = new HashSet<CreateAnswerDto>();
        }

        public string AddedByUserId { get; set; }

        public string QuestionValue { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int DifficultId { get; set; }

        public ICollection<CreateAnswerDto> Answers { get; set; }
    }
}
