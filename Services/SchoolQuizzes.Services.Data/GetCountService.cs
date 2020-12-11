namespace SchoolQuizzes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<Question> questions;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<ApplicationRole> roles;
        private readonly IDeletableEntityRepository<Quiz> quizes;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IDeletableEntityRepository<Answer> answers;
        private readonly IDeletableEntityRepository<Category> categories;

        public GetCountService(IDeletableEntityRepository<Question> questions, IDeletableEntityRepository<Answer> answers, IDeletableEntityRepository<Category> categories, IDeletableEntityRepository<ApplicationUser> users, IDeletableEntityRepository<Quiz> quizes, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.questions = questions;
            this.users = users;
            this.quizes = quizes;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.answers = answers;
            this.categories = categories;
        }

        public DashboardIndexViewModel GetCounts()
        {
            var data = new DashboardIndexViewModel()
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
            ApplicationRole role = await roleManager.FindByNameAsync(GlobalConstants.StudentRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }

        public async Task<int> GetTeachersCountAsync()
        {
            ApplicationRole role = await roleManager.FindByNameAsync(GlobalConstants.TeacherRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }

        public async Task<int> GetAdminsCountAsync()
        {
            ApplicationRole role = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
            string roleId = role.Id;
            return this.users.All()
                .Where(x => x.Roles.Any(r => r.RoleId == roleId)).Count();
        }
    }
}
