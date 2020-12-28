namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Data.Models;

    [Area("Administration")]
    public class StagesController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public StagesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Administration/Stages
        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.Stages.ToListAsync());
        }

        // GET: Administration/Stages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var stage = await this._context.Stages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stage == null)
            {
                return this.NotFound();
            }

            return this.View(stage);
        }

        // GET: Administration/Stages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Stage stage)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(stage);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(stage);
        }

        // GET: Administration/Stages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var stage = await this._context.Stages.FindAsync(id);
            if (stage == null)
            {
                return this.NotFound();
            }
            return this.View(stage);
        }

        // POST: Administration/Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Stage stage)
        {
            if (id != stage.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(stage);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.StageExists(stage.Id))
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
            return this.View(stage);
        }

        // GET: Administration/Stages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var stage = await this._context.Stages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stage == null)
            {
                return this.NotFound();
            }

            return this.View(stage);
        }

        // POST: Administration/Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage = await this._context.Stages.FindAsync(id);
            this._context.Stages.Remove(stage);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool StageExists(int id)
        {
            return this._context.Stages.Any(e => e.Id == id);
        }
    }
}
