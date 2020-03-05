using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_API.Models;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleanersController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public CleanersController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: api/Cleaners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cleaner>>> GetCleaners()
        {
            return await _context.Cleaners.ToListAsync();
        }

        // GET: api/Cleaners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cleaner>> GetCleaner(int id)
        {
            var cleaner = await _context.Cleaners.FindAsync(id);

            if (cleaner == null)
            {
                return NotFound();
            }

            return cleaner;
        }

        // PUT: api/Cleaners/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaner(int id, Cleaner cleaner)
        {
            if (id != cleaner.CleanerId)
            {
                return BadRequest();
            }

            _context.Entry(cleaner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleanerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cleaners
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cleaner>> PostCleaner(Cleaner cleaner)
        {
            _context.Cleaners.Add(cleaner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaner", new { id = cleaner.CleanerId }, cleaner);
        }

        // DELETE: api/Cleaners/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cleaner>> DeleteCleaner(int id)
        {
            var cleaner = await _context.Cleaners.FindAsync(id);
            if (cleaner == null)
            {
                return NotFound();
            }

            _context.Cleaners.Remove(cleaner);
            await _context.SaveChangesAsync();

            return cleaner;
        }

        private bool CleanerExists(int id)
        {
            return _context.Cleaners.Any(e => e.CleanerId == id);
        }
    }
}
