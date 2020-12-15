namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class CreateClassRoomViewModel
    {
        [Required]
        [Display(Name = "Клас")]
        public int StageId { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public int CategoryId { get; set; }

        public SelectList Stages { get; set; }

        public SelectList Categories { get; set; }
    }
}
