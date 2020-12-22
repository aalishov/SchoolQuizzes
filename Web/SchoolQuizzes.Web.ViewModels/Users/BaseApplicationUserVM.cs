namespace SchoolQuizzes.Web.ViewModels.Users
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseApplicationUserVM : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [Display(Name = "Училище")]
        public string School { get; set; }


        [Display(Name = "Дата на раждане")]
        public string DateOfBirth { get; set; }


        [Display(Name = "Електронна поща")]
        public string Email { get; set; }


        [Display(Name = "Мобилен телефон")]
        public string PhoneNumber { get; set; }
    }
}
