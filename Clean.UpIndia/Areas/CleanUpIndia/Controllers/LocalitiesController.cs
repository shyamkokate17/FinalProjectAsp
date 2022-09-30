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
using System.Data;

namespace Clean.UpIndia.Areas.CleanUpIndia.Controllers
{
    [Area("CleanUpIndia")]
    [Authorize(Roles = "AppAdmin")]       // Checked if user has logged in and Is a User with Admin Role
    public class LocalitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CleanUpIndia/Localities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Localities.ToListAsync());
        }

        // GET: CleanUpIndia/Localities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities
                .FirstOrDefaultAsync(m => m.LocalityId == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // GET: CleanUpIndia/Localities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CleanUpIndia/Localities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocalityId,LocalityName")] Locality locality)
        {
            locality.LocalityName = locality.LocalityName.Trim(); // removes addtional spaces and trim data

            bool isDuplicateExists = _context.Localities.Any(m => m.LocalityName == locality.LocalityName);
            if (isDuplicateExists)
            {
                ModelState.AddModelError("LocalityName", "Duplicate City found!!! Please add new one...");
            }


            if (ModelState.IsValid)
            {
                _context.Add(locality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locality);
        }

        // GET: CleanUpIndia/Localities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }
            return View(locality);
        }

        // POST: CleanUpIndia/Localities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocalityId,LocalityName")] Locality locality)
        {
            if (id != locality.LocalityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.LocalityId))
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
            return View(locality);
        }

        // GET: CleanUpIndia/Localities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Localities
                .FirstOrDefaultAsync(m => m.LocalityId == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // POST: CleanUpIndia/Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locality = await _context.Localities.FindAsync(id);
            _context.Localities.Remove(locality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
            return _context.Localities.Any(e => e.LocalityId == id);
        }

        
    }
}
