﻿namespace SchoolQuizzes.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SchoolQuizzes.Web.ViewModels.Answers;

    public class QuestionQuizViewModel
    {
        public QuestionQuizViewModel()
        {
            this.Answers = new HashSet<AnswerQuizViewModel>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<AnswerQuizViewModel> Answers { get; set; }
    }
}
