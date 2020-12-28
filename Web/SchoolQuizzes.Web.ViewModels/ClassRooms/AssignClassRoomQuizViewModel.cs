namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    using System.ComponentModel.DataAnnotations;

    public class AssignClassRoomQuizViewModel
    {
        public int ClassRoomId { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Тест")]
        public int QuizId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Трудност")]
        public int DifficultId { get; set; }

        [Display(Name = "Изпит")]
        public bool IsExam { get; set; }

        public SelectList Quizzes { get; set; }

        public SelectList Difficults { get; set; }

    }
}
