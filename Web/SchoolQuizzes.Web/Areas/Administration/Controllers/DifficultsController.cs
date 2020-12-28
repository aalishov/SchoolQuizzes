namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Data.Models;

    [Area("Administration")]
    public class DifficultsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public DifficultsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Administration/Difficults
        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.DifficultLevels.ToListAsync());
        }

        // GET: Administration/Difficults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var difficult = await this._context.DifficultLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficult == null)
            {
                return this.NotFound();
            }

            return this.View(difficult);
        }

        // GET: Administration/Difficults/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Difficults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Difficult difficult)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(difficult);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(difficult);
        }

        // GET: Administration/Difficults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var difficult = await this._context.DifficultLevels.FindAsync(id);
            if (difficult == null)
            {
                return this.NotFound();
            }
            return this.View(difficult);
        }

        // POST: Administration/Difficults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Difficult difficult)
        {
            if (id != difficult.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(difficult);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DifficultExists(difficult.Id))
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
            return this.View(difficult);
        }

        // GET: Administration/Difficults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var difficult = await this._context.DifficultLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficult == null)
            {
                return this.NotFound();
            }

            return this.View(difficult);
        }

        // POST: Administration/Difficults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var difficult = await this._context.DifficultLevels.FindAsync(id);
            this._context.DifficultLevels.Remove(difficult);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DifficultExists(int id)
        {
            return this._context.DifficultLevels.Any(e => e.Id == id);
        }
    }
}
