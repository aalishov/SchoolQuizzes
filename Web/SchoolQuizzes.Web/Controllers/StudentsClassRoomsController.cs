namespace SchoolQuizzes.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.StudentsClassRooms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class StudentsClassRoomsController : BaseController
    {
        private readonly IClassRoomsService roomsService;
        private readonly ICategoriesService categoriesService;
        private readonly IStagesService stagesService;
        private readonly IUsersService usersService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuizzesService quizzesService;
        private readonly ITakesService takesService;

        public StudentsClassRoomsController(IClassRoomsService roomsService, ICategoriesService categoriesService, IStagesService stagesService, IUsersService usersService, IDifficultsService difficultsService, IQuizzesService quizzesService, ITakesService takesService)
        {
            this.roomsService = roomsService;
            this.categoriesService = categoriesService;
            this.stagesService = stagesService;
            this.usersService = usersService;
            this.difficultsService = difficultsService;
            this.quizzesService = quizzesService;
            this.takesService = takesService;
        }

        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IndexStudentClassRoomsVM model = new IndexStudentClassRoomsVM();
            model.ClassRooms = this.roomsService.GetRooms<IndexStudentClassRoomVM>(userId);
            return this.View(model);
        }

        public IActionResult Details(int roomId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DetailsUserClassRoomVM model = this.roomsService.GetRoomById<DetailsUserClassRoomVM>(roomId);
            return this.View(model);
        }

        public async Task <IActionResult> Start(int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.takesService.CreateTakeAsync(userId, id);
            return this.RedirectToAction("Take", "TakesQuiz", new { id = id });
        }
    }
}
