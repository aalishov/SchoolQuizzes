namespace SchoolQuizzes.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Web.ViewModels.ClassRooms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;


    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class ClassRoomsController : Controller
    {
        private readonly IClassRoomsService roomsService;
        private readonly ICategoriesService categoriesService;
        private readonly IStagesService stagesService;
        private readonly IUsersService usersService;

        public ClassRoomsController(IClassRoomsService roomsService, ICategoriesService categoriesService, IStagesService stagesService, IUsersService usersService)
        {
            this.roomsService = roomsService;
            this.categoriesService = categoriesService;
            this.stagesService = stagesService;
            this.usersService = usersService;
        }
        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IndexUserClassRooms model = new IndexUserClassRooms();
            model.ClassRooms = this.roomsService.GetRooms<IndexClassRoom>(userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateClassRoomViewModel model = new CreateClassRoomViewModel
            {
                Categories = this.categoriesService.GetAllAsSelectList(),
                Stages = this.stagesService.GetAllAsSelectList(),
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClassRoomViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Categories = this.categoriesService.GetAllAsSelectList();
                inputModel.Stages = this.stagesService.GetAllAsSelectList();
                return this.View(inputModel);
            }

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.roomsService.CreateClassRoomAsync(userId, inputModel.StageId, inputModel.CategoryId);

            return this.Redirect("/ClassRooms/Index");
        }

        public IActionResult Details(int roomId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DetailsClassRoomViewModel model = new DetailsClassRoomViewModel();
            model = this.roomsService.GerRoomById<DetailsClassRoomViewModel>(roomId);
            return View(model);
        }

        public IActionResult AddStudent(int roomId)
        {
            int stageId = this.roomsService.GetRoomStageId(roomId);
            ICollection<AddStudentInClassRoomViewModel> model = this.usersService.GetAllStudentsByStageId<AddStudentInClassRoomViewModel>(roomId, stageId);
            ViewData["RoomId"] = roomId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(int studentId, int roomId)
        {
            await this.roomsService.AddStudentToClassRoomAsync(roomId, studentId);

            return this.RedirectToAction( "Details", "ClassRooms", new { roomId=roomId });
        }
    }
}
