using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Data.Contracts;
using SchoolQuizzes.Services.Data.ModelsDto;
using SchoolQuizzes.Web.ViewModels.Quizzes;
using System.Linq;
using System.Security.Claims;

namespace SchoolQuizzes.Web.ViewComponents
{
    [ViewComponent(Name = "UserRole")]
    public class UserRoleViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserRoleViewComponent(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await this.userManager.GetUserAsync(this.UserClaimsPrincipal);
            var userRole = this.userManager.GetRolesAsync(currentUser).Result.FirstOrDefault().ToLower();
            this.ViewData["UserRole"] = userRole;

            return this.View();
        }
    }
}
