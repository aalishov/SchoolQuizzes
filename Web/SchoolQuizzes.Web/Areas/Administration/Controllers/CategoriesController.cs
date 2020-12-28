﻿namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Administration.Categories;

    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Services.Data;
    using SchoolQuizzes.Services.Data.Contracts;

    [Area("Administration")]
    public class CategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<ApplicationUser> users, ICategoriesService categoriesService)
        {
            this.categories = categories;
            this.users = users;
            this.categoriesService = categoriesService;
        }

        // GET: Administration/Categories
        public async Task<IActionResult> Index(int page = 1)
        {
            IndexCategoriesViewModel model = new IndexCategoriesViewModel();
            model.Categories = await this.categoriesService.GetAllForPagination<CategoryViewModel>(page, model.ItemsPerPage);
            model.ElementsCount = this.categories.AllWithDeleted().Count();
            model.PageNumber = page;

            return this.View(model);
        }

        // GET: Administration/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            CategoryViewModel category = await this.GetCategoryAsync(id);

            return category == null ? this.NotFound() : (IActionResult)this.View(category);
        }

        // GET: Administration/Categories/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.users.AllAsNoTracking(), "Id", "UserName");
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (this.ModelState.IsValid)
            {
                await this.categories.AddAsync(category);
                _ = await this.categories.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            //this.ViewData["AddedByUserId"] = new SelectList(this.users.AllAsNoTracking(), "Id", "UserName");
            return this.View(category);
        }

        // GET: Administration/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            CategoryViewModel category = await this.GetCategoryAsync(id);
            if (category == null)
            {
                return this.NotFound();
            }

            //this.ViewData["AddedByUserId"] = new SelectList(this.users.AllAsNoTracking(), "Id", "UserName");
            return this.View(category);
        }

        // POST: Administration/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.categories.Update(category);
                    _ = await this.categories.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(category.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            //this.ViewData["AddedByUserId"] = new SelectList(this.users.AllAsNoTracking(), "Id", "UserName");
            return this.View(category);
        }

        // GET: Administration/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            CategoryViewModel category = await this.GetCategoryAsync(id);
            return category == null ? this.NotFound() : (IActionResult)this.View(category);
        }


        // POST: Administration/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = await this.categories.All().FirstOrDefaultAsync(x => x.Id == id);
            this.categories.Delete(category);
            _ = await this.categories.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            Category category = await this.categories.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            category.IsDeleted = false;
            this.categories.Update(category);
            _ = await this.categories.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CategoryExists(int id)
        {
            return this.categories.All().Any(e => e.Id == id);
        }

        private async Task<CategoryViewModel> GetCategoryAsync(int? id)
        {
            return await this.categories.All().To<CategoryViewModel>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
