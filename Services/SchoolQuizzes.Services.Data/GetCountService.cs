namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<Question> questions;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Quiz> quizes;
        private readonly IDeletableEntityRepository<Take> takes;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ITakesService takesService;
        private readonly IDeletableEntityRepository<Answer> answers;
        private readonly IDeletableEntityRepository<Category> categories;

        public GetCountService(IDeletableEntityRepository<Question> questions, IDeletableEntityRepository<Answer> answers, IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<ApplicationUser> users, IDeletableEntityRepository<Quiz> quizes, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IDeletableEntityRepository<Take> takes, ITakesService takesService)
        {
            this.questions = questions;
            this.users = users;
            this.quizes = quizes;
            this.roleManager = roleManager;
            this.takesService = takesService;
            this.answers = answers;
            this.categories = categories;
            this.takes = takes;
        }

        public DashboardIndexViewModel GetCounts()
        {
            DashboardIndexViewModel data = new DashboardIndexViewModel()
            {
                UsersCount = this.users.All().Count(),
                QuestionsCount = this.questions.All().Count(),
                AnswersCount = this.answers.All().Count(),
                CategoriesCount = this.categories.All().Count(),
                QuizzesCount = this.quizes.All().Count(),
            };

            return data;
        }

        public async Task<int> GetStudentsCountAsync()
        {
            ApplicationRole role = await this.roleManager.FindByNameAsync(GlobalConstants.StudentRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }

        public async Task<int> GetTeachersCountAsync()
        {
            ApplicationRole role = await this.roleManager.FindByNameAsync(GlobalConstants.TeacherRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }

        public async Task<int> GetAdminsCountAsync()
        {
            ApplicationRole role = await this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }

        public int GetCorrectAnswerCount()
        {
            int sum = 0;
            List<Take> takes = this.takes.AllAsNoTracking().ToList();
            foreach (Take take in takes)
            {
                sum += this.takesService.GetCorrectAnswerCountByTakeId(take.Id);
            }
            return sum;
        }

        public int GetInCorrectAnswerCount()
        {
            int sum = 0;
            List<Take> takes = this.takes.AllAsNoTracking().ToList();
            foreach (Take take in takes)
            {
                sum += this.takesService.GetInCorrectAnswerCountByTakeId(take.Id);
            }
            return sum;
        }
    }
}
