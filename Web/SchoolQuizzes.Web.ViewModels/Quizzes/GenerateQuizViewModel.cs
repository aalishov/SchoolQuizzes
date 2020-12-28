namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class GenerateQuizViewModel : IMapTo<Quiz>
    {


        [Required]
        [MinLength(4)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Трудност")]
        public int DifficultId { get; set; }


        [Required]
        [Display(Name = "Клас")]
        public int StageId { get; set; }

        [Required]
        [Display(Name = "Брой въпроси")]
        [Range(3, 15)]
        public int Count { get; set; }

        public string AddedByUserId { get; set; }

        public SelectList StagesItems { get; set; }

        public SelectList CategoriesItems { get; set; }

        public SelectList DifficultsItems { get; set; }

        public ICollection<Question> Questions { get; set; }

    }
}
