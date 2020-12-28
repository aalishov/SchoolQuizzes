namespace SchoolQuizzes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Questions;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class QuestionsController : BaseController
    {
        private readonly ISelectListsService listsService;
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(ISelectListsService listsService, IQuestionsService questionsService, UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.userManager = userManager;
            this.listsService = listsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateQuestionViewModel model = new CreateQuestionViewModel
            {
                CategoriesItems = this.listsService.GetAllCategoriesAsSelectList(),
                DifficultsItems = this.listsService.GetAllDifficultsAsSelectList(),
                StagesItems=this.listsService.GetAllStagesAsSelectList(),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.listsService.GetAllCategoriesAsSelectList();
                inputModel.DifficultsItems = this.listsService.GetAllDifficultsAsSelectList();
                inputModel.StagesItems = this.listsService.GetAllStagesAsSelectList();
                return this.View(inputModel);
            }

            inputModel.UserId = this.userManager.GetUserId(this.User);

            await this.questionsService.CreateAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
