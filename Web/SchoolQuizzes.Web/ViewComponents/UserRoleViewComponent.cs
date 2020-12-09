namespace SchoolQuizzes.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Data.Models;

    [ViewComponent(Name = "UserRole")]
    public class UserRoleViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRoleViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.UserClaimsPrincipal);
            string userRole = this.userManager.GetRolesAsync(currentUser).Result.FirstOrDefault().ToLower();
            this.ViewData["UserRole"] = userRole;

            return this.View();
        }
    }
}
