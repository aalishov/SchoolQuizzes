namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Quizzes;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public class TakesService : ITakesService
    {
        private readonly IQuizzesService quizzesService;
        private readonly IAnswersService answersService;
        private readonly IQuestionsService questionsService;

        public TakesService(IQuizzesService quizzesService, IAnswersService answersService, IQuestionsService questionsService)
        {
            this.quizzesService = quizzesService;

            this.answersService = answersService;
            this.questionsService = questionsService;
        }

        public TakeQuestionAnswerViewModel GetExamQuestion(int page, int num = 1)
        {
            int itemsPerPage = 1;
            Quiz quiz = this.quizzesService.GetQuizById(2);
            ICollection<Question> questions = this.questionsService.GetQuestionsByQuizId(2);
            Question question = questions
                .OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).FirstOrDefault();

            TakeQuestionAnswerViewModel model = new TakeQuestionAnswerViewModel();
            model.ItemsPerPage = itemsPerPage;
            model.Title = quiz.Title;
            model.QuizId = quiz.Id;
            model.CurrentQuestionId = question.Id;
            model.Question = question.Value;
            model.PageNumber = page;
            model.ElementsCount = questions.Count();

            ICollection<Answer> answers = this.answersService.GetQuestionAnswersById(model.CurrentQuestionId);
            foreach (var answer in answers)
            {
                model.Answers.Add(answer.Value);
            }

            return model;
        }
    }
}
