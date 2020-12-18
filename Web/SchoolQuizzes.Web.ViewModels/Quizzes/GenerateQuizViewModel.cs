namespace SchoolQuizzes.Web.ViewModels.Quizzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GenerateQuizViewModel
    {

        [Required]
        [MinLength(4)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Трудност")]
        public int DifficultId { get; set; }

        [Display(Name = "Брой въпроси")]
        [Range(3, 15)]
        public int Count { get; set; }

        public ICollection<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public ICollection<KeyValuePair<string, string>> DifficultsItems { get; set; }

    }
}
