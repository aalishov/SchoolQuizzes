namespace SchoolQuizzes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Web.ViewModels;
    using SchoolQuizzes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                UsersCount = this.db.Users.Count(),
                QuestionsCount = this.db.Questions.Count(),
                AnswersCount = this.db.Answers.Count(),
                CategoriesCount = this.db.Categories.Count(),
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
