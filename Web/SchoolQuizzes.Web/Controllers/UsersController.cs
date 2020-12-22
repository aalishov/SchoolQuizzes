namespace SchoolQuizzes.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Users;

    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IStagesService stagesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, IStagesService stagesService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.stagesService = stagesService;
            this.userManager = userManager;
            this.mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task<IActionResult> IndexStudent()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            StudentIndexViewModel model = new StudentIndexViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            model.ApplicationUser = this.mapper.Map<ApplicationUser, BaseApplicationUserVM>(user, model.ApplicationUser);
            model.Student = this.usersService.GetStudentByUserId<BaseStudentVM>(userId);
            return this.View(model);
        }

        public async Task<IActionResult> EditStudent(string userId)
        {
            EditStudentViewModel model = new EditStudentViewModel();
            model.ApplicationUser = this.mapper.Map<ApplicationUser, BaseApplicationUserVM>(await this.userManager.GetUserAsync(this.User), model.ApplicationUser);
            model.Student = this.usersService.GetStudentByUserId<BaseStudentVM>(userId);
            model.Student.Stages = this.stagesService.GetAllAsSelectList();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditStudentViewModel model)
        {
            await this.usersService.UpdateStudentByUserIdAsync(model);
            return this.RedirectToAction("IndexStudent");
        }
    }
}
