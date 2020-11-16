namespace SchoolQuizzes.Services.Data
{
    using System.Linq;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;

    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<Question> questions;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Quiz> quizes;
        private readonly IDeletableEntityRepository<Answer> answers;
        private readonly IDeletableEntityRepository<Category> categories;

        public GetCountService(IDeletableEntityRepository<Question> questions, IDeletableEntityRepository<Answer> answers, IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<ApplicationUser> users, IDeletableEntityRepository<Quiz> quizes)
        {
            this.questions = questions;
            this.users = users;
            this.quizes = quizes;
            this.answers = answers;
            this.categories = categories;
        }

        public CountDto GetCounts()
        {
            var data = new CountDto()
            {
                UsersCount = this.users.All().Count(),
                QuestionsCount = this.questions.All().Count(),
                AnswersCount = this.answers.All().Count(),
                CategoriesCount = this.categories.All().Count(),
                QuizzesCount = this.quizes.All().Count(),
            };

            return data;
        }
    }
}
