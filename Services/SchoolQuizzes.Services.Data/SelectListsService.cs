namespace SchoolQuizzes.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SelectListsService : ISelectListsService
    {
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly IDeletableEntityRepository<Difficult> difficults;
        private readonly IDeletableEntityRepository<Stage> stages;

        public SelectListsService(IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<Difficult> difficults, IDeletableEntityRepository<Stage> stages)
        {
            this.categories = categories;
            this.difficults = difficults;
            this.stages = stages;
        }

        public SelectList GetAllCategoriesAsSelectList()
        {
            return new SelectList(this.categories.AllAsNoTracking(), "Id", "Name");
        }

        public SelectList GetAllDifficultsAsSelectList()
        {
            return new SelectList(this.difficults.AllAsNoTracking(), "Id", "Name");
        }

        public SelectList GetAllStagesAsSelectList()
        {
            return new SelectList(this.stages.AllAsNoTracking(), "Id", "Name");
        }
    }
}
