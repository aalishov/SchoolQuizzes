namespace SchoolQuizzes.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Takes;

    [Authorize]
    public class TakesQuizController : BaseController
    {
        private readonly IQuizzesService quizzesService;
        private readonly ITakesService takesService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public TakesQuizController(IQuizzesService quizzesService, ITakesService takesService, UserManager<ApplicationUser> userManager)
        {
            this.quizzesService = quizzesService;
            this.takesService = takesService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.takesService.IsUserHasNotFinishedQuiz(this.userManager.GetUserId(this.User)))
            {
                return this.RedirectToAction("IndexContinue");
            }
            else
            {
                var model = new SelectTakeIndexViewModel();
                model.QuizzesItems = this.quizzesService.GetAllAsKeyValuePairs();
                return this.View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(SelectTakeIndexViewModel model)
        {
            await this.takesService.CreateTakeAsync(this.userManager.GetUserId(this.User), model.QuizId);
            return this.RedirectToAction("Take");
        }

        [HttpGet]
        public IActionResult IndexContinue()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Take(int id = 1)
        {
            TakeQuestionAnswerViewModel model = this.takesService.GetExamQuestion(this.userManager.GetUserId(this.User), id);
            model.Action = "Take";
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Take(TakeQuestionAnswerViewModel input)
        {
            await this.takesService.SaveTakedAnswerAsync(this.userManager.GetUserId(this.User), input.CurrentQuestionId, input.UserAnswerId);

            if (input.PageNumber + 1 > input.QuizQuestionsCount)
            {
                input.PageNumber = 0;
            }

            return this.RedirectToAction("Take", new RouteValueDictionary(
                     new { controller = "TakesQuiz", action = "Take", Id = ++input.PageNumber }));
        }

        [HttpGet]
        public async Task<IActionResult> Finish(int id)
        {
            await this.takesService.FinishQuizAsync(id);

            FinishTakeViewModel model = new FinishTakeViewModel();
            model.TakeId = id;
            model.Result = this.takesService.GetResult(id);

            return this.View(model);
        }

        public IActionResult MyTakes()
        {
            ICollection<UserTakeViewModel> model = this.takesService.GetUserTakesByUserId(this.userManager.GetUserId(this.User));

            return this.View(model);
        }
    }
}
