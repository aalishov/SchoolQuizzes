namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        public string GetCategoryNameById(int id);

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public SelectList GetAllAsSelectList();
    }
}
