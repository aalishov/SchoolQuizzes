namespace SchoolQuizzes.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class QuizzesController : BaseController
    {
        private readonly IAnswersService answersService;
        private readonly ICategoriesService categoriesService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuestionsService questionsService;
        private readonly IQuizzesService quizzesService;

        public QuizzesController(IAnswersService answersService, ICategoriesService categoriesService, IDifficultsService difficultsService, IQuestionsService questionsService, IQuizzesService quizzesService)
        {
            this.answersService = answersService;
            this.difficultsService = difficultsService;
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.quizzesService = quizzesService;
        }

        public IActionResult Generate()
        {
            var model = new GenerateQuizViewModel();
            model.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            model.DifficultsItems = this.difficultsService.GetAllAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Generate(GenerateQuizViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                inputModel.DifficultsItems = this.difficultsService.GetAllAsKeyValuePairs();
                return this.View(inputModel);
            }

            GenerateQuizDto quizDto = new GenerateQuizDto
            {
                Title = inputModel.Title,
                DifficultId = inputModel.DifficultId,
                CategoryId = inputModel.CategoryId,
                Count = inputModel.Count,
                UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            };

            await this.quizzesService.CreateAsync(quizDto);
            return this.Redirect("/Quizzes/Index");
        }

        public IActionResult Index()
        {
            var quizzes = this.quizzesService.GetQuizzes();

            IndexQuizViewModel model = new IndexQuizViewModel();
            foreach (var q in quizzes)
            {
                model.Quizzes.Add(new DetailsQuizViewModel()
                {
                    Id = q.Id,
                    Title = q.Title,
                    DifficultName = this.difficultsService.GetDifficultNameById(q.DifficultId),
                    CategoryName = this.categoriesService.GetCategoryNameById(q.CategoryId),
                });
            }

            return this.View(model);
        }

        public IActionResult Details(int id)
        {
            DetailsQuizViewModel model = this.quizzesService.GetQuizWithQuestionsAndAnswersById(id);

            return this.View(model);
        }
    }
}
