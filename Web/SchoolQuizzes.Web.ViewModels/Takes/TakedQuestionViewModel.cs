namespace SchoolQuizzes.Web.ViewModels.Takes
{
    using AutoMapper;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.Linq;

    public class TakedQuestionViewModel : IMapFrom<TakedAnswer>, IHaveCustomMappings
    {
        public int TakeId { get; set; }

        public string QuestionValue { get; set; }

        public string AnswerValue { get; set; }

        public bool AnswerIsCorrect { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TakedAnswer, TakedQuestionViewModel>()
               .ForMember(x => x.AnswerIsCorrect, opt =>
                   opt.MapFrom(p =>
                       p.Answer.Questions.FirstOrDefault(s => s.AnswerId == p.AnswerId).IsCorrect));
        }
    }
}
