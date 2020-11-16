namespace SchoolQuizzes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Questions;
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    [Authorize]
    public class QuizzesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuestionsService questionsService;
        private readonly IQuizzesService quizzesService;

        public QuizzesController(ICategoriesService categoriesService, IDifficultsService difficultsService, IQuestionsService questionsService, IQuizzesService quizzesService)
        {
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.quizzesService = quizzesService;
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

            await this.quizzesService.CreateAsync(quizDto);
            return this.Redirect("/");
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
                    Difficult = this.difficultsService.GetDifficultNameById(q.DifficultId),
                    Category = this.categoriesService.GetCategoryNameById(q.CategoryId),
                });
            }

            return this.View(model);
        }

        public IActionResult Details(int id)
        {
            var quiz = this.quizzesService.GetQuizById(id);
            var questions = this.questionsService.GetQuestionsByQuizId(id);

            DetailsQuizViewModel model = new DetailsQuizViewModel();

            model.Title = quiz.Title;
            model.Difficult = this.difficultsService.GetDifficultNameById(quiz.DifficultId);
            model.Category = this.categoriesService.GetCategoryNameById(quiz.CategoryId);
            foreach (var q in questions)
            {
                model.Questions.Add(new QuestionQuizViewModel { QuestionValue = q.QuestionValue });
            }

            return this.View(model);
        }
    }
}
