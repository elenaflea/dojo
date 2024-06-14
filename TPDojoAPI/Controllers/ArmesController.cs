using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPDojoAPI;
using TPDojoAPI.BO;
using TPDojoAPI.DTO;

namespace TPDojoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmesController : ControllerBase
    {
        private readonly HelloWorldDbContext _context;

        public ArmesController(HelloWorldDbContext context)
        {
            _context = context;
        }

        // GET: api/Armes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arme>>> GetArmes()
        {
            return await _context.Armes.ToListAsync();
        }

        // GET: api/Armes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arme>> GetArme(int id)
        {
            var arme = await _context.Armes.FindAsync(id);

            if (arme == null)
            {
                return NotFound();
            }

            return arme;
        }

        // PUT: api/Armes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArme(int id, Arme arme)
        {
            if (id != arme.Id)
            {
                return BadRequest();
            }

            _context.Entry(arme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmeExists(id))
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

        // POST: api/Armes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArmeDto>> PostArme(ArmeDto dto)
        {

            Arme entite = new Arme();
            entite.Id = dto.Id;
            entite.Nom = dto.Nom;
            entite.Degat = dto.Degat;

            _context.Armes.Add(entite);
            await _context.SaveChangesAsync();

            dto.Id = entite.Id; 
           // return CreatedAtAction("GetArme", new { id = arme.Id }, arme);
           return this.Ok(dto);
        }

        // DELETE: api/Armes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArme(int id)
        {
            var arme = await _context.Armes.FindAsync(id);
            if (arme == null)
            {
                return NotFound();
            }

            _context.Armes.Remove(arme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArmeExists(int id)
        {
            return _context.Armes.Any(e => e.Id == id);
        }
    }
}
