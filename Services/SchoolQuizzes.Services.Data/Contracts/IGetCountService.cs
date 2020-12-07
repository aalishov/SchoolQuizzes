namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    public interface IGetCountService
    {
        DashboardIndexViewModel GetCounts();
    }
}
