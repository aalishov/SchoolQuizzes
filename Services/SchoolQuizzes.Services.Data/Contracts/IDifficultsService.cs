namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IDifficultsService
    {
        ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
