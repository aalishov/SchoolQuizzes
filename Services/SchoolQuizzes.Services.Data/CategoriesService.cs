namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categories;

        public CategoriesService(IDeletableEntityRepository<Category> categories)
        {
            this.categories = categories;
        }

        public async Task AddACategoryAsync(Category category)
        {
            await this.categories.AddAsync(category);
            await this.categories.SaveChangesAsync();
        }

        public bool CategoryExists(int id)
        {
            return this.categories.AllAsNoTracking().Any(e => e.Id == id);
        }

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categories.AllAsNoTracking()
                  .Select(x => new { x.Id, x.Name })
                  .ToList()
                  .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                  .ToList();
        }

        public SelectList GetAllAsSelectList()
        {
            return new SelectList(this.categories.AllAsNoTracking(), "Id", "Name");
        }

        public async Task<ICollection<T>> GetAllForPagination<T>(int page, int itemsPerPage)
        {
            return await this.categories.AllWithDeleted()
                  .OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage)
                  .Take(itemsPerPage)
                  .To<T>()
                 .ToListAsync();
        }

        public async Task<T> GetCategoryAsync<T>(int id)
        {
            return await this.categories.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public int GetCategoriesCount()
        {
            return this.categories.AllAsNoTracking().Count();
        }

        public string GetCategoryNameById(int id)
        {
            return this.categories.AllAsNoTracking().FirstOrDefault(x => x.Id == id).Name;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            this.categories.Undelete(category);
            await this.categories.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categories.All().FirstOrDefault(x => x.Id == id);
            this.categories.Delete(category);
            await this.categories.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            Category category = await this.categories.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            category.IsDeleted = false;
            this.categories.Update(category);
            _ = await this.categories.SaveChangesAsync();
        }


    }
}
