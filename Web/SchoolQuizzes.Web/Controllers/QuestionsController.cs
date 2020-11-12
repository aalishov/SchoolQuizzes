namespace SchoolQuizzes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IQuestionsService questionsService;

        public QuestionsController(ICategoriesService categoriesService, IQuestionsService questionsService)
        {
            this.categoriesService = categoriesService;
            this.questionsService = questionsService;
        }

        public IActionResult Create()
        {
            var model = new CreateQuestionViewModel();
            model.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel model)
        {

            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(model);
            }

            CreateQuestionDto questionDto = new CreateQuestionDto()
            {
                QuestionValue = model.QuestionValue,
                CategoryId = model.CategoryId,
                DifficultId = model.DifficultId,
                Description = model.Description,
            };

            foreach (var answer in model.Answers)
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
