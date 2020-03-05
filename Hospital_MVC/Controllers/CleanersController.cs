using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_API.Models;

namespace Hospital_MVC.Controllers
{
    public class CleanersController : Controller
    {
        private readonly HospitalDbContext _context;

        public CleanersController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Cleaners
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Cleaners.Include(c => c.Hospital);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Cleaners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaner = await _context.Cleaners
                .Include(c => c.Hospital)
                .FirstOrDefaultAsync(m => m.CleanerId == id);
            if (cleaner == null)
            {
                return NotFound();
            }

            return View(cleaner);
        }

        // GET: Cleaners/Create
        public IActionResult Create()
        {
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId");
            return View();
        }

        // POST: Cleaners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CleanerId,CleanerName,HospitalId")] Cleaner cleaner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cleaner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", cleaner.HospitalId);
            return View(cleaner);
        }

        // GET: Cleaners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaner = await _context.Cleaners.FindAsync(id);
            if (cleaner == null)
            {
                return NotFound();
            }
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", cleaner.HospitalId);
            return View(cleaner);
        }

        // POST: Cleaners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CleanerId,CleanerName,HospitalId")] Cleaner cleaner)
        {
            if (id != cleaner.CleanerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cleaner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CleanerExists(cleaner.CleanerId))
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
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "HospitalId", "HospitalId", cleaner.HospitalId);
            return View(cleaner);
        }

        // GET: Cleaners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cleaner = await _context.Cleaners
                .Include(c => c.Hospital)
                .FirstOrDefaultAsync(m => m.CleanerId == id);
            if (cleaner == null)
            {
                return NotFound();
            }

            return View(cleaner);
        }

        // POST: Cleaners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cleaner = await _context.Cleaners.FindAsync(id);
            _context.Cleaners.Remove(cleaner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CleanerExists(int id)
        {
            return _context.Cleaners.Any(e => e.CleanerId == id);
        }
    }
}
