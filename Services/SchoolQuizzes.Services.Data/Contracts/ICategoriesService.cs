namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        public string GetCategoryNameById(int id);

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
