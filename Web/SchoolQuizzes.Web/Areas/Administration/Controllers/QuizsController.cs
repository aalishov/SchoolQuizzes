using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolQuizzes.Data;
using SchoolQuizzes.Data.Models;

namespace SchoolQuizzes.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class QuizsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public QuizsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Administration/Quizs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this._context.Quizzes.Include(q => q.AddedByUser).Include(q => q.Category).Include(q => q.Difficult);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var quiz = await this._context.Quizzes
                .Include(q => q.AddedByUser)
                .Include(q => q.Category)
                .Include(q => q.Difficult)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return this.NotFound();
            }

            return this.View(quiz);
        }

        // GET: Administration/Quizs/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this._context.Users, "Id", "Id");
            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name");
            this.ViewData["DifficultId"] = new SelectList(this._context.DifficultLevels, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddedByUserId,Title,CategoryId,DifficultId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Quiz quiz)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(quiz);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            this.ViewData["AddedByUserId"] = new SelectList(this._context.Users, "Id", "Id", quiz.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name", quiz.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this._context.DifficultLevels, "Id", "Name", quiz.DifficultId);
            return this.View(quiz);
        }

        // GET: Administration/Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var quiz = await this._context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return this.NotFound();
            }
            this.ViewData["AddedByUserId"] = new SelectList(this._context.Users, "Id", "Id", quiz.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name", quiz.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this._context.DifficultLevels, "Id", "Name", quiz.DifficultId);
            return this.View(quiz);
        }

        // POST: Administration/Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddedByUserId,Title,CategoryId,DifficultId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(quiz);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.QuizExists(quiz.Id))
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
            this.ViewData["AddedByUserId"] = new SelectList(this._context.Users, "Id", "Id", quiz.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this._context.Categories, "Id", "Name", quiz.CategoryId);
            this.ViewData["DifficultId"] = new SelectList(this._context.DifficultLevels, "Id", "Name", quiz.DifficultId);
            return this.View(quiz);
        }

        // GET: Administration/Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var quiz = await this._context.Quizzes
                .Include(q => q.AddedByUser)
                .Include(q => q.Category)
                .Include(q => q.Difficult)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return this.NotFound();
            }

            return this.View(quiz);
        }

        // POST: Administration/Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await this._context.Quizzes.FindAsync(id);
            this._context.Quizzes.Remove(quiz);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool QuizExists(int id)
        {
            return this._context.Quizzes.Any(e => e.Id == id);
        }
    }
}
