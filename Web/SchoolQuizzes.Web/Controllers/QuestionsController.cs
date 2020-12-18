﻿namespace SchoolQuizzes.Web.Controllers
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

        [HttpGet]
        public IActionResult Create()
        {
            CreateQuestionViewModel model = new CreateQuestionViewModel
            {
                CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs(),
                DifficultsItems = this.difficultsService.GetAllAsKeyValuePairs(),
            };
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

            inputModel.UserId = this.userManager.GetUserId(this.User);

            await this.questionsService.CreateAsync(inputModel);

            return this.Redirect("/");
        }
    }
}
