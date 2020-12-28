namespace SchoolQuizzes.Web.Controllers
{
    using System.IO;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class QuizzesController : BaseController
    {
        private readonly IExportService exportService;
        private readonly IQuizzesService quizzesService;
        private readonly ISelectListsService selectListsService;

        public QuizzesController(IExportService exportService, IQuizzesService quizzesService, ISelectListsService selectListsService)
        {

            this.exportService = exportService;
            this.quizzesService = quizzesService;
            this.selectListsService = selectListsService;
        }

        public IActionResult Generate()
        {
            var model = new GenerateQuizViewModel();

            model.CategoriesItems = this.selectListsService.GetAllCategoriesAsSelectList();
            model.DifficultsItems = this.selectListsService.GetAllDifficultsAsSelectList();
            model.StagesItems = this.selectListsService.GetAllStagesAsSelectList();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Generate(GenerateQuizViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.selectListsService.GetAllCategoriesAsSelectList();
                inputModel.DifficultsItems = this.selectListsService.GetAllDifficultsAsSelectList();
                inputModel.StagesItems = this.selectListsService.GetAllStagesAsSelectList();
                return this.View(inputModel);
            }
            inputModel.AddedByUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.quizzesService.CreateAsync(inputModel);
            return this.Redirect("/Quizzes/Index");
        }

        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return this.NotFound();
            }

            IndexQuizViewModel model = new IndexQuizViewModel();

            model.ElementsCount = this.quizzesService.GetQuizzesCount();
            model.PageNumber = (int)page;
            model.Quizzes = this.quizzesService.GetQuizzes<DetailsQuizViewModel>((int)page, model.ItemsPerPage);

            return this.View(model);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            DetailsQuizViewModel model = this.quizzesService.GetQuizWithQuestionsAndAnswersById(id);

            return this.View(model);
        }

        public IActionResult ExportQuizQuestions(int id)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            DetailsQuizViewModel model = this.quizzesService.GetQuizWithQuestionsAndAnswersById(id);

            MemoryStream stream = this.exportService.ExportQuizQuestions(model);

            //Download Word document in the browser
            return this.File(stream, "application/msword", $"{model.Title}.docx");
        }


    }
}
