namespace SchoolQuizzes.Services.Data.ModelsDto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateAnswerDto
    {
        public string AnswerValue { get; set; }

        public string Description { get; set; }

        public string IsTrue { get; set; }
    }
}
