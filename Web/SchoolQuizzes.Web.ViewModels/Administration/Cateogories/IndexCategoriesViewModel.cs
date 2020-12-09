using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Mapping;

using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolQuizzes.Web.ViewModels.Administration.Categories
{
    public class IndexCategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string AddedByUserId { get; set; }

        [Display(Name = "Добавен от")]
        public string AddedByUserUserName { get; set; }

        [Display(Name = "Категория")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата на изтриване")]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Дата на създаване")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Последно променен")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Изтрит")]
        public bool IsDeleted { get; set; }
    }
}
