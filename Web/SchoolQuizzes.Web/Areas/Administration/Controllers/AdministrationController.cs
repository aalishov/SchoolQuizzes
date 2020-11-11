namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
