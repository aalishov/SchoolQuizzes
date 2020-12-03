namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using System.Collections.Generic;
    using AutoMapper;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;
    using SchoolQuizzes.Web.ViewModels.Shared;

    public class TakeQuestionAnswerViewModel : PagingViewModel, IMapFrom<Take>, IHaveCustomMappings
    {
        public TakeQuestionAnswerViewModel()
        {
            this.Answers = new List<AnswerViewModel>();
        }

        public int Id { get; set; }

        public int QuizId { get; set; }

        public int CurrentQuestionId { get; set; }

        public string QuizTitle { get; set; }

        public string Question { get; set; }

        public int QuizQuestionsCount { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public int UserAnswerId { get; set; }

        public int? TakenAnswer { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<int, TakeQuestionAnswerViewModel>()
                .ForMember(x => x.ItemsPerPage, opt => opt.DoNotUseDestinationValue())
                .BeforeMap((s, d) => d.ItemsPerPage = 1);
        }
    }
}
