using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPDojoAPI;
using TPDojoAPI.BO;

namespace TPDojoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtMartialsController : ControllerBase
    {
        private readonly HelloWorldDbContext _context;

        public ArtMartialsController(HelloWorldDbContext context)
        {
            _context = context;
        }

        // GET: api/ArtMartials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtMartial>>> GetArtMartial()
        {
            return await _context.ArtMartial.ToListAsync();
        }

        // GET: api/ArtMartials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtMartial>> GetArtMartial(int id)
        {
            var artMartial = await _context.ArtMartial.FindAsync(id);

            if (artMartial == null)
            {
                return NotFound();
            }

            return artMartial;
        }

        // PUT: api/ArtMartials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtMartial(int id, ArtMartial artMartial)
        {
            if (id != artMartial.Id)
            {
                return BadRequest();
            }

            _context.Entry(artMartial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtMartialExists(id))
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

        // POST: api/ArtMartials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArtMartial>> PostArtMartial(ArtMartial artMartial)
        {
            _context.ArtMartial.Add(artMartial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtMartial", new { id = artMartial.Id }, artMartial);
        }

        // DELETE: api/ArtMartials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtMartial(int id)
        {
            var artMartial = await _context.ArtMartial.FindAsync(id);
            if (artMartial == null)
            {
                return NotFound();
            }

            _context.ArtMartial.Remove(artMartial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtMartialExists(int id)
        {
            return _context.ArtMartial.Any(e => e.Id == id);
        }
    }
}
