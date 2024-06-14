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
    public class SamouraisController : ControllerBase
    {
        private readonly HelloWorldDbContext _context;

        public SamouraisController(HelloWorldDbContext context)
        {
            _context = context;
        }

        // GET: api/Samourais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Samourai>>> GetSamourais()
        {
            return await _context.Samourais.ToListAsync();
        }

        // GET: api/Samourais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Samourai>> GetSamourai(int id)
        {
            var samourai = await _context.Samourais.FindAsync(id);

            if (samourai == null)
            {
                return NotFound();
            }

            return samourai;
        }

        // PUT: api/Samourais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSamourai(int id, Samourai samourai)
        {
            if (id != samourai.Id)
            {
                return BadRequest();
            }

            _context.Entry(samourai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SamouraiExists(id))
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

        // POST: api/Samourais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Samourai>> PostSamourai(Samourai samourai)
        {
            _context.Samourais.Add(samourai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSamourai", new { id = samourai.Id }, samourai);
        }

        // DELETE: api/Samourais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSamourai(int id)
        {
            var samourai = await _context.Samourais.FindAsync(id);
            if (samourai == null)
            {
                return NotFound();
            }

            _context.Samourais.Remove(samourai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SamouraiExists(int id)
        {
            return _context.Samourais.Any(e => e.Id == id);
        }
    }
}
