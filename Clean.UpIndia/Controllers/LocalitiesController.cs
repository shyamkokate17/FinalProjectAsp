using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clean.UpIndia.Data;
using Clean.UpIndia.Models;
using Microsoft.Extensions.Logging;

namespace Clean.UpIndia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // private readonly ILogger<LocalitiesController> _logger;
       

        public LocalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Localities
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Locality>>> GetLocalities()
         public async Task<IActionResult> GetLocalities()
        {
            try
            {
                var localities = await _context
                            .Localities.ToListAsync();
                if (localities == null)
                {
                    return NotFound();
                }

                return Ok(localities);
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }

         }

        // GET: api/Localities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locality>> GetLocality(int id)
        {
            var locality = await _context.Localities.FindAsync(id);

            if (locality == null)
            {
                return NotFound();
            }

            return locality;
        }

        // PUT: api/Localities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocality(int id, Locality locality)
        {
            if (id != locality.LocalityId)
            {
                return BadRequest();
            }

            _context.Entry(locality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalityExists(id))
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

        // POST: api/Localities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Locality>> PostLocality(Locality locality)
        {
            try
            {
                _context.Localities.Add(locality);
                await _context.SaveChangesAsync();
                var result = CreatedAtAction("GetLocality", new { id = locality.LocalityId }, locality);

                return Ok(result);
            }

            catch(Exception exp)
            {
                ModelState.AddModelError("POST", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Localities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Locality>> DeleteLocality(int id)
        {
            try
            {
                var locality = await _context.Localities.FindAsync(id);
                if (locality == null)
                {
                    return NotFound();
                }

                _context.Localities.Remove(locality);
                await _context.SaveChangesAsync();

                return Ok(locality);

            }
            catch(System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);

            }
        }

        private bool LocalityExists(int id)
        {
            return _context.Localities.Any(e => e.LocalityId == id);
        }
    }
}
