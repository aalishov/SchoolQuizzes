namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IDifficultsService
    {
        string GetDifficultNameById(int id);

        ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
