using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SchoolQuizzes.Services.Data.Contracts;
using SchoolQuizzes.Web.ViewModels.Quizzes;
using SchoolQuizzes.Web.ViewModels.Takes;
using System.Linq;

namespace SchoolQuizzes.Web.Controllers
{
    public class TakesQuizController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly ITakesService takesService;

        public TakesQuizController(IQuizzesService quizzesService, ITakesService takesService)
        {
            this.quizzesService = quizzesService;
            this.takesService = takesService;
        }

        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            // TODO: Make id to come from somewhere
            string model = this.quizzesService.GetGuizNameById(id);
            this.ViewBag.Title = model;
            return this.View();
        }

        [HttpGet]
        public IActionResult Take(int id = 1)
        {
            TakeQuestionAnswerViewModel model = this.takesService.GetExamQuestion(id);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Take(TakeQuestionAnswerViewModel input)
        {
            return this.Json(input);
            //return this.RedirectToAction("Take", new RouteValueDictionary(
            //         new { controller = "TakesQuiz", action = "Take", Id = ++input.PageNumber }));
        }
    }
}
