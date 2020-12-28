namespace SchoolQuizzes.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SchoolQuizzes.Common;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Messaging;
    using SchoolQuizzes.Web.ViewModels.ClassRooms;

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;


    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class ClassRoomsController : Controller
    {
        private readonly IClassRoomsService roomsService;
        private readonly ICategoriesService categoriesService;
        private readonly IStagesService stagesService;
        private readonly IUsersService usersService;
        private readonly IDifficultsService difficultsService;
        private readonly ITakesService takesService;
        private readonly IQuizzesService quizzesService;
        private readonly IEmailSender emailSender;

        public ClassRoomsController(IClassRoomsService roomsService, ICategoriesService categoriesService, IStagesService stagesService, IUsersService usersService, IDifficultsService difficultsService, ITakesService takesService, IQuizzesService quizzesService, IEmailSender emailSender)
        {
            this.roomsService = roomsService;
            this.categoriesService = categoriesService;
            this.stagesService = stagesService;
            this.usersService = usersService;
            this.difficultsService = difficultsService;
            this.takesService = takesService;
            this.quizzesService = quizzesService;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IndexUserClassRooms model = new IndexUserClassRooms();
            model.ClassRooms = this.roomsService.GetRooms<IndexClassRoom>(userId);
            return this.View(model);
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
            model = this.roomsService.GetRoomById<DetailsClassRoomViewModel>(roomId);
            return this.View(model);
        }

        public IActionResult ТакеDetails(int classRoomQuizId)
        {
            ClassRoomQuizDetails model = this.roomsService.GetQuizTakesDetails<ClassRoomQuizDetails>(classRoomQuizId);
            foreach (var t in model.Takes)
            {
                t.Result = this.takesService.GetResult(t.Id);
            }
            return this.View(model);
        }

        public IActionResult AddStudent(int roomId)
        {
            int stageId = this.roomsService.GetRoomStageId(roomId);
            ICollection<AddStudentInClassRoomViewModel> model = this.usersService.GetAllStudentsByStageId<AddStudentInClassRoomViewModel>(roomId, stageId);
            this.ViewData["RoomId"] = roomId;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(int studentId, int roomId)
        {
            await this.roomsService.AddStudentToClassRoomAsync(roomId, studentId);

            return this.RedirectToAction("Details", "ClassRooms", new { roomId = roomId });
        }

        [HttpGet]
        public IActionResult AssignClassRoomQuiz(int classRoomId, int difficultId = -1)
        {
            int categoryId = this.roomsService.GetRoomById<IndexClassRoom>(classRoomId).CategoryId;
            AssignClassRoomQuizViewModel model = new AssignClassRoomQuizViewModel();
            model.Difficults = this.difficultsService.GetAllAsSelectList();
            model.Quizzes = this.quizzesService.GetQuizzesByCategoryAndDifficultAsSelectList(categoryId, difficultId);
            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AssignClassRoomQuiz(AssignClassRoomQuizViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Difficults = this.difficultsService.GetAllAsSelectList();
                model.Quizzes = this.quizzesService.GetQuizzesByCategoryAndDifficultAsSelectList(model.CategoryId, model.DifficultId);
            }

            await this.roomsService.AssignClassRoomQuizAsync(model.Title, model.ClassRoomId, model.QuizId, model.IsExam);

            return this.RedirectToAction("Details", "ClassRooms", new { roomId = model.ClassRoomId });
        }

        public async Task<IActionResult> AssignQuizNotification(int classRoomId)
        {
            var emails = this.roomsService.GetStudentsEmail(classRoomId);
            ClassRoomAssignQuizNotificationVM quiz = this.roomsService.GetClassRoomQuiz<ClassRoomAssignQuizNotificationVM>(classRoomId);


            await this.emailSender.SendEmailAsync("schoolquizzes@abv.bg", "Alishov", "a.alishov@live.com", "DemoEmail", "<h1>Имате възложен тест: Demo send email</h1>");
            //foreach (var email in emails)
            //{
            //    var html = new StringBuilder();
            //    html.AppendLine($"<h1>Имате възложен тест: {quiz.Title}</h1>");
            //    await this.emailSender.SendEmailAsync($"schoolquizzes@abv.bg", $"{quiz.ClassRoomTeacherApplicationUserLasttName}", $"{email}", quiz.Title, html.ToString());
            //}

            return this.RedirectToAction(nameof(this.Details), "ClassRooms", new { roomId = classRoomId });
        }
    }
}
