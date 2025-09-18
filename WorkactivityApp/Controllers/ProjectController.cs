using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkactivityApp.Data;
using WorkactivityApp.Models;
using WorkactivityApp.Models.ViewModels;

namespace WorkactivityApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ProjectController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: Project
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.Include(p => p.Users).ToListAsync());
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View(new Project
            {
                Time = new Time()
            });
        }


        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,Description,Time")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,Description,Time")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(project);
                    _context.Entry(project).State = EntityState.Modified;

                    // Explicitly mark the owned type 'Time' as modified
                    _context.Entry(project).Reference(p => p.Time).TargetEntry.State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(project);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddProjectTime(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            ViewData["ProjectId"] = id; 
            return View("AddProjectTime");

        }

        [HttpPost]
        public async Task<IActionResult> AddProjectTime(int id, AddedTimeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var project = await _context.Projects
                .Include(p => p.Time)
                .ThenInclude(t => t.AddedTimes)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null || project.Time == null)
                return NotFound();

            var addedTime = new AddedTime
            {
                StartAddedTime = model.StartAddedTime,
                EndAddedTime = model.EndAddedTime
            };

            project.Time.AddedTimes.Add(addedTime);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddUser(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            var users = await _userManager.Users.ToListAsync();

            ViewData["Users"] = new SelectList(users, "Id", "UserName");
            ViewData["ProjectId"] = id; 

            return View("AddUserToProject");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToProject(int projectId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var project = await _context.Projects
                                        .Include(p => p.Users)
                                        .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (user == null || project == null)
                return NotFound();

            if (!project.Users.Any(u => u.Id == userId))
            {
                project.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Project");
        }


        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
