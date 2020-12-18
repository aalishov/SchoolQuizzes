namespace SchoolQuizzes.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels;
    using SchoolQuizzes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGetCountService getCount;

        public HomeController(IGetCountService getCount)
        {
            this.getCount = getCount;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                StudentsCount = await this.getCount.GetStudentsCountAsync(),
                TeachersCount = await this.getCount.GetTeachersCountAsync(),
                AdminCount = await this.getCount.GetAdminsCountAsync(),
                CorrectAnswersCount = this.getCount.GetCorrectAnswerCount(),
                InCorrectAnswersCount = this.getCount.GetInCorrectAnswerCount(),
            };
            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
