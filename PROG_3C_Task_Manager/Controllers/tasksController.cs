using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG_3C_Task_Manager.Data;
using PROG_3C_Task_Manager.Models;

namespace PROG_3C_Task_Manager.Controllers
{
    public class tasksController : Controller
    {
        private readonly PROG_3C_Task_ManagerContext _context;

        public tasksController(PROG_3C_Task_ManagerContext context)
        {
            _context = context;
        }

        // GET: tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.task.ToListAsync();

            // Mark tasks as overdue
            foreach (var task in tasks)
            {
                if (task.Duration < DateTime.Now)
                {
                    task.StatusMessage = "Overdue";
                }
            }

            return View(tasks);
        }

        // GET: tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.task.FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            // Mark as overdue if past due date
            if (task.Duration < DateTime.Now)
            {
                ViewBag.IsOverdue = true;
            }
            else
            {
                ViewBag.IsOverdue = false;
            }

            return View(task);
        }

        // GET: tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Priority,Status,Duration,StatusMessage")] task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Priority,Status,Duration,StatusMessage")] task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!taskExists(task.Id))
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
            return View(task);
        }

        // GET: tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.task.FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.task.FindAsync(id);
            if (task != null)
            {
                _context.task.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool taskExists(int id)
        {
            return _context.task.Any(e => e.Id == id);
        }
    }
}
