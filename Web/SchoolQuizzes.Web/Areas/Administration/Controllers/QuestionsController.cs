namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Data.Models;

    [Area("Administration")]
    public class QuestionsController : AdministrationController
    {
        private readonly ApplicationDbContext context;

        public QuestionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.Questions.Include(q => q.AddedByUser).Include(q => q.Category).Include(q => q.Difficult).Include(q => q.Stage);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var question = await this.context.Questions
                .Include(q => q.AddedByUser)
                .Include(q => q.Category)
                .Include(q => q.Difficult)
                .Include(q => q.Stage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return this.NotFound();
            }

            return this.View(question);
        }

        // GET: Administration/Questions/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.context.Users, "Id", "Id");
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Name");
            this.ViewData["DifficultId"] = new SelectList(this.context.DifficultLevels, "Id", "Name");
            this.ViewData["StageId"] = new SelectList(this.context.Stages, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddedByUserId,Value,Description,CategoryId,DifficultId,StageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Question question)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(question);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            this.ViewData["AddedByUserId"] = new SelectList(this.context.Users, "Id", "Id", question.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Name", question.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this.context.DifficultLevels, "Id", "Name", question.DifficultId);
            this.ViewData["StageId"] = new SelectList(this.context.Stages, "Id", "Id", question.StageId);
            return this.View(question);
        }

        // GET: Administration/Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var question = await this.context.Questions.FindAsync(id);
            if (question == null)
            {
                return this.NotFound();
            }
            this.ViewData["AddedByUserId"] = new SelectList(this.context.Users, "Id", "Id", question.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Name", question.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this.context.DifficultLevels, "Id", "Name", question.DifficultId);
            this.ViewData["StageId"] = new SelectList(this.context.Stages, "Id", "Id", question.StageId);
            return this.View(question);
        }

        // POST: Administration/Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddedByUserId,Value,Description,CategoryId,DifficultId,StageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Question question)
        {
            if (id != question.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(question);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.QuestionExists(question.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.RedirectToAction(nameof(this.Index));
            }
            this.ViewData["AddedByUserId"] = new SelectList(this.context.Users, "Id", "Id", question.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Name", question.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this.context.DifficultLevels, "Id", "Name", question.DifficultId);
            this.ViewData["StageId"] = new SelectList(this.context.Stages, "Id", "Id", question.StageId);
            return this.View(question);
        }

        // GET: Administration/Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var question = await this.context.Questions
                .Include(q => q.AddedByUser)
                .Include(q => q.Category)
                .Include(q => q.Difficult)
                .Include(q => q.Stage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return this.NotFound();
            }

            return this.View(question);
        }

        // POST: Administration/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await this.context.Questions.FindAsync(id);
            this.context.Questions.Remove(question);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool QuestionExists(int id)
        {
            return this.context.Questions.Any(e => e.Id == id);
        }
    }
}
