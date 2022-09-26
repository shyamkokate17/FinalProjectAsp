using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clean.UpIndia.Data;
using Clean.UpIndia.Models;

namespace Clean.UpIndia.Areas.CleanUpIndia.Controllers
{
    [Area("CleanUpIndia")]
    public class PublicToiletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicToiletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CleanUpIndia/PublicToilets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PublicToilets.Include(p => p.Localities);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CleanUpIndia/PublicToilets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicToilet = await _context.PublicToilets
                .Include(p => p.Localities)
                .FirstOrDefaultAsync(m => m.ToiletId == id);
            if (publicToilet == null)
            {
                return NotFound();
            }

            return View(publicToilet);
        }

        // GET: CleanUpIndia/PublicToilets/Create
        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName");
            return View();
        }

        // POST: CleanUpIndia/PublicToilets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToiletId,OpenHours,IsAvailable,Rating,LocalityId")] PublicToilet publicToilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicToilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", publicToilet.LocalityId);
            return View(publicToilet);
        }

        // GET: CleanUpIndia/PublicToilets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicToilet = await _context.PublicToilets.FindAsync(id);
            if (publicToilet == null)
            {
                return NotFound();
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", publicToilet.LocalityId);
            return View(publicToilet);
        }

        // POST: CleanUpIndia/PublicToilets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToiletId,OpenHours,IsAvailable,Rating,LocalityId")] PublicToilet publicToilet)
        {
            if (id != publicToilet.ToiletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicToilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicToiletExists(publicToilet.ToiletId))
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
            ViewData["LocalityId"] = new SelectList(_context.Localities, "LocalityId", "LocalityName", publicToilet.LocalityId);
            return View(publicToilet);
        }

        // GET: CleanUpIndia/PublicToilets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicToilet = await _context.PublicToilets
                .Include(p => p.Localities)
                .FirstOrDefaultAsync(m => m.ToiletId == id);
            if (publicToilet == null)
            {
                return NotFound();
            }

            return View(publicToilet);
        }

        // POST: CleanUpIndia/PublicToilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicToilet = await _context.PublicToilets.FindAsync(id);
            _context.PublicToilets.Remove(publicToilet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicToiletExists(int id)
        {
            return _context.PublicToilets.Any(e => e.ToiletId == id);
        }
    }
}
