namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
