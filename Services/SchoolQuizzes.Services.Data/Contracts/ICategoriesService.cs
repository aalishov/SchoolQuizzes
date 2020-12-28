namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        public Task AddACategoryAsync(Category category);

        public bool CategoryExists(int id);

        public Task DeleteAsync(int id);

        public string GetCategoryNameById(int id);

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public Task<ICollection<T>> GetAllForPagination<T>(int page, int itemsPerPage);

        public Task<T> GetCategoryAsync<T>(int id);

        public int GetCategoriesCount();

        public SelectList GetAllAsSelectList();

        public Task RestoreAsync(int id);

        public Task UpdateCategoryAsync(Category category);

        
    }
}
