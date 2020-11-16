namespace SchoolQuizzes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Quizis;

    public class QuizzesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuestionsService questionsService;
        private readonly IQuizzesService quizisService;

        public QuizzesController(ICategoriesService categoriesService, IDifficultsService difficultsService, IQuestionsService questionsService, IQuizzesService quizisService)
        {
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.quizisService = quizisService;
            this.difficultsService = difficultsService;
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
                Questions = this.questionsService.GetQuestionsForQuiz(inputModel.CategoryId, inputModel.DifficultId, inputModel.Count),
            };

            await this.quizisService.CreateAsync(quizDto);
            return this.Redirect("/");
        }

        public IActionResult Index()
        {
            IndexQuizViewModel model = new IndexQuizViewModel();
            model.Quizzes = this.quizisService.GetQuizzes();
            return this.View(model);
        }
    }
}
