using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clean.UpIndia.Data;
using Clean.UpIndia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clean.UpIndia.Areas.CleanUpIndia.Controllers
{
    [Area("CleanUpIndia")]
    [Authorize(Roles = "AppAdmin")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CleanUpIndia/Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Events.Include(z => z.Locality);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CleanUpIndia/Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Events
                .Include(z => z.Locality)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // GET: CleanUpIndia/Events/Create
        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName");
            return View();
        }

        // POST: CleanUpIndia/Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDescription,EventDate,LocalityId")] Event function)
        {
            if (ModelState.IsValid)
            {
                _context.Add(function);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", function.LocalityId);
            return View(function);
        }

        // GET: CleanUpIndia/Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Events.FindAsync(id);
            if (function == null)
            {
                return NotFound();
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", function.LocalityId);
            return View(function);
        }

        // POST: CleanUpIndia/Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDescription,EventDate,LocalityId")] Event function)
        {
            if (id != function.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(function);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(function.EventId))
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
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", function.LocalityId);
            return View(function);
        }

        // GET: CleanUpIndia/Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var function = await _context.Events
                .Include(z => z.Locality)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // POST: CleanUpIndia/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var function = await _context.Events.FindAsync(id);
            _context.Events.Remove(function);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
