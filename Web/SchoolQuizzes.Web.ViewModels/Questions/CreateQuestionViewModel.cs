namespace SchoolQuizzes.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;

    public class CreateQuestionViewModel : IMapTo<Question>
    {
        public CreateQuestionViewModel()
        {
            this.Answers = new List<CreateAnswerViewModel>();
        }

        [Required]
        [MinLength(4)]
        [Display(Name = "Въпрос")]
        public string QuestionValue { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Трудност")]
        public int DifficultId { get; set; }


        [Required]
        [Display(Name = "Клас")]
        public int StageId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Отговори:")]
        [Ignore]
        public ICollection<CreateAnswerViewModel> Answers { get; set; }

        public SelectList CategoriesItems { get; set; }

        public SelectList DifficultsItems { get; set; }

        public SelectList StagesItems { get; set; }

        
    }
}
