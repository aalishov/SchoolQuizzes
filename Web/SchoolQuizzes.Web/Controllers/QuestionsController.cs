namespace SchoolQuizzes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Questions;

    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(ICategoriesService categoriesService, IDifficultsService difficultsService, IQuestionsService questionsService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
            this.userManager = userManager;
            this.difficultsService = difficultsService;
        }

        public IActionResult Create()
        {
            var model = new CreateQuestionViewModel();
            model.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            model.DifficultsItems = this.difficultsService.GetAllAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                inputModel.DifficultsItems = this.difficultsService.GetAllAsKeyValuePairs();
                return this.View(inputModel);
            }

            CreateQuestionDto questionDto = new CreateQuestionDto()
            {
                QuestionValue = inputModel.QuestionValue,
                CategoryId = inputModel.CategoryId,
                DifficultId = inputModel.DifficultId,
                Description = inputModel.Description,
                AddedByUserId = this.userManager.GetUserId(this.User),
            };

            foreach (var answer in inputModel.Answers)
            {
                questionDto.Answers.Add(new CreateAnswerDto
                {
                    AnswerValue = answer.AnswerValue,
                    Description = answer.Description,
                    IsTrue = answer.IsTrue,
                });
            }

            await this.questionsService.CreateAsync(questionDto);

            return this.Redirect("/");
        }
    }
}
