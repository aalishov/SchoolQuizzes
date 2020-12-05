namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = 20 };
            return this.View(viewModel);
        }
    }
}
