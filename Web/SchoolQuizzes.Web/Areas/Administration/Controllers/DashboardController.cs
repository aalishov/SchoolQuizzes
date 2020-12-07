namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IGetCountService getCount;

        public DashboardController(IGetCountService getCount)
        {
            this.getCount = getCount;
        }

        public IActionResult Index()
        {
            var viewModel = this.getCount.GetCounts();

            return this.View(viewModel);
        }
    }
}
