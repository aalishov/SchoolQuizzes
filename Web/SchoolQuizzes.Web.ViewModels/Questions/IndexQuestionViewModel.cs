namespace SchoolQuizzes.Web.ViewModels.Questions
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexQuestionViewModel:IMapFrom<Question>
    {
        public IndexQuestionViewModel()
        {
            this.Answers = new HashSet<IndexQuestionsAnswerViewModel>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<IndexQuestionsAnswerViewModel> Answers { get; set; }

    }
}
