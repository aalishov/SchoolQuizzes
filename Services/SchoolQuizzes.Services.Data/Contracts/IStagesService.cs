namespace SchoolQuizzes.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public interface IStagesService
    {
        public SelectList GetAllAsSelectList();
    }
}
