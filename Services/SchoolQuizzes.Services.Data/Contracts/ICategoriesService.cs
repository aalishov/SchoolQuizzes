namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        string GetCategoryNameById(int id);

        ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
