namespace SchoolQuizzes.Web.ViewModels.Administration.Categories
{
    using SchoolQuizzes.Web.ViewModels.Shared;

    using System.Collections.Generic;

    public class IndexCategoriesViewModel : PagingViewModel
    {
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
