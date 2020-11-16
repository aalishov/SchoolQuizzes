namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categories;

        public CategoriesService(IDeletableEntityRepository<Category> categories)
        {
            this.categories = categories;
        }

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categories.AllAsNoTracking()
                  .Select(x => new { x.Id, x.Name })
                  .ToList()
                  .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                  .ToList();
        }

        public string GetCategoryNameById(int id)
        {
            return this.categories.AllAsNoTracking().FirstOrDefault(x => x.Id == id).Name;
        }
    }
}
