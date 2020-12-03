namespace SchoolQuizzes.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;

    public class QuestionQuizViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public QuestionQuizViewModel()
        {
            this.Answers = new HashSet<AnswerViewModel>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Question, QuestionQuizViewModel>()
                .ForMember(x => x.Answers, opt => opt.Ignore());
        }

    }
}
