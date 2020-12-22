namespace SchoolQuizzes.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    public class StagesService : IStagesService
    {
        private readonly IDeletableEntityRepository<Stage> stages;

        public StagesService(IDeletableEntityRepository<Stage> stages)
        {
            this.stages = stages;
        }

        public SelectList GetAllAsSelectList()
        {
            return new SelectList(this.stages.AllAsNoTracking(), "Id", "Name");
        }
    }
}
