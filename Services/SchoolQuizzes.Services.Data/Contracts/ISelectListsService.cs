namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ISelectListsService
    {
        public SelectList GetAllDifficultsAsSelectList();

        public SelectList GetAllCategoriesAsSelectList();

        public SelectList GetAllStagesAsSelectList();
    }
}
