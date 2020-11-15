namespace SchoolQuizzes.Web.ViewModels.Questions
{
    using SchoolQuizzes.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateQuestionViewModel
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

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Трудност")]
        public int DifficultId { get; set; }

        [Display(Name = "Отговори:")]
        public ICollection<CreateAnswerViewModel> Answers { get; set; }

        public ICollection<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public ICollection<KeyValuePair<string, string>> DifficultsItems { get; set; }
    }
}
