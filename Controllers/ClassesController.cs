using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2LINQ.Data;
using Labb2LINQ.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;

namespace Labb2LINQ.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index( string searchString)
        {

            ViewData["CurrentFilter"] = searchString;

            var classes = from e in _context.Class.Include(c => c.Course).Include(s => s.Student).Include(t => t.Teacher)
            select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(e => e.Course.Title.Contains(searchString)
                                       || e.Teacher.FirstName.Contains(searchString)
                                       || e.Teacher.LastName.Contains(searchString)
                                       || e.Student.FirstName.Contains(searchString)
                                       || e.Student.LastName.Contains(searchString));
                                       
            }
            return View(await classes.AsNoTracking().ToListAsync());
        }

         //GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(c => c.Course)
                .Include(s => s.Student)
                .Include(t => t.Teacher)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewData["FKCourseId"] = new SelectList(_context.Course, "CourseId", "CourseId");
            ViewData["FKStudentId"] = new SelectList(_context.Student, "StudentId", "StudentId");
            ViewData["FKTeacherId"] = new SelectList(_context.Teacher, "TeacherId", "TeacherId");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,FKCourseId,FKStudentId,FKTeacherId,Grade")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKCourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", @class.FKCourseId);
            ViewData["FKStudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", @class.FKStudentId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teacher, "TeacherId", "TeacherId", @class.FKTeacherId);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["FKCourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", @class.FKCourseId);
            ViewData["FKStudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", @class.FKStudentId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teacher, "TeacherId", "TeacherId", @class.FKTeacherId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,FKCourseId,FKStudentId,FKTeacherId,Grade")] Class @class)
        {
            if (id != @class.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.ClassId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FKCourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", @class.FKCourseId);
            ViewData["FKStudentId"] = new SelectList(_context.Student, "StudentId", "StudentId", @class.FKStudentId);
            ViewData["FKTeacherId"] = new SelectList(_context.Teacher, "TeacherId", "TeacherId", @class.FKTeacherId);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(c => c.Course)
                .Include(s => s.Student)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Class == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Class'  is null.");
            }
            var @class = await _context.Class.FindAsync(id);
            if (@class != null)
            {
                _context.Class.Remove(@class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
          return (_context.Class?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}
