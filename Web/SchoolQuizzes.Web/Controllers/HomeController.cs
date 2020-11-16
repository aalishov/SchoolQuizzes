namespace SchoolQuizzes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

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

        public IActionResult Index()
        {
            var countDto = this.getCount.GetCounts();

            var viewModel = new IndexViewModel()
            {
                UsersCount = countDto.UsersCount,
                AnswersCount = countDto.AnswersCount,
                CategoriesCount = countDto.CategoriesCount,
                QuestionsCount = countDto.QuestionsCount,
                QuzziesCount = countDto.QuizzesCount,
            };

            return this.View(viewModel);
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
