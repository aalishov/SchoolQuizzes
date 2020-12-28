namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISelectListsService
    {
        public SelectList GetAllDifficultsAsSelectList();

        public SelectList GetAllCategoriesAsSelectList();

        public SelectList GetAllStagesAsSelectList();
    }
}
