namespace SchoolQuizzes.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    public interface IStagesService
    {
        public SelectList GetAllAsSelectList();
    }
}
