namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    using System.Collections.Generic;

    public interface IDifficultsService
    {
        string GetDifficultNameById(int id);

        ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public SelectList GetAllAsSelectList();
    }
}
