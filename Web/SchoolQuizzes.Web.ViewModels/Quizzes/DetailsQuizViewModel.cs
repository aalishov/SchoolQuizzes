namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public class DetailsQuizViewModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public DetailsQuizViewModel()
        {
            this.Questions = new List<QuestionQuizViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Трудност: ")]
        public string DifficultName { get; set; }

        [Display(Name = "Категория: ")]
        public string CategoryName { get; set; }

        public ICollection<QuestionQuizViewModel> Questions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Quiz, DetailsQuizViewModel>()
                .ForMember(x => x.Questions, opt => opt.Ignore());
        }
    }
}
