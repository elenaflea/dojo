using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPDojoWeb;
using TPDojoWeb.BO;
using TPDojoWeb.Migrations;
using TPDojoWeb.Models;

namespace TPDojoWeb.Controllers
{
    public class SamouraisController : Controller
    {
        private readonly HelloWorldDbContext _context;

        public SamouraisController(HelloWorldDbContext context)
        {
            _context = context;
        }

        // GET: Samourais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Samourais.Include(s => s.Arme).Include(s => s.ArtMartials).ToListAsync());
        }

        // GET: Samourais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samourai = await _context.Samourais.Include(s => s.Arme).Include(s => s.ArtMartials)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samourai == null)
            {
                return NotFound();
            }

            return View(samourai);
        }

        // GET: Samourais/Create
        public IActionResult Create()
        {
            //  return View();
            SamouraisVM samouraisVM = new SamouraisVM();
            samouraisVM.SelectArmes = new SelectList(_context.Armes.ToList(), "Id", "Nom");
            samouraisVM.SelectArtsMartiaux = new MultiSelectList(_context.ArtMartial.ToList(), "Id", "Nom");
            return View(samouraisVM);
        }

        // POST: Samourais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Force, ArmeId")] Models.SamouraisVM samouraiVM)
        {

            if (!ModelState.IsValid && samouraiVM == null)
            {
                return View(samouraiVM);

            }
            else 
            {

                Samourai samourai = new Samourai
                {
                    Nom = samouraiVM.Nom,
                    Force = samouraiVM.Force,
                    Arme = (samouraiVM.ArmeId == null) ? null : _context.Armes.Find(samouraiVM.ArmeId),
                    ArtMartials= (samouraiVM.ArtsMartiauxIds == null) ? null : _context.ArtMartial.Where(a => samouraiVM.ArtsMartiauxIds.Contains(a.Id)).ToList()
                };

                await _context.AddAsync(samourai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          //  return View(samouraiVM);
        }

        // GET: Samourais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samourai = await _context.Samourais.FindAsync(id);
            if (samourai == null)
            {
                return NotFound();
            }

            SamouraisVM samouraisVM = new SamouraisVM();
            samouraisVM.SelectArmes = new SelectList(_context.Armes.ToList(), "Id", "Nom");
            samouraisVM.Id = samourai.Id;
            samouraisVM.Nom = samourai.Nom;
            samouraisVM.Force = samourai.Force;
            samouraisVM.ArmeId = (samourai.Arme == null) ? null : samourai.Arme.Id;
     

            return View(samouraisVM);

        }

        // POST: Samourais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Force, ArmeId")] Models.SamouraisVM samouraiVM)
        {
            if (id != samouraiVM.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid && samouraiVM == null)
            {
                return View(samouraiVM);

            }
            else 
            {
                try
                {
                  //  _context.Update(samouraiVM);
                    Samourai? samourai = _context.Samourais.Find(id);

                    samourai.Nom = samouraiVM.Nom;
                    samourai.Force = samouraiVM.Force;
                    samourai.Arme = (samouraiVM.ArmeId == null) ? null : _context.Armes.Find(samouraiVM.ArmeId);
                    samourai.ArtMartials = (samouraiVM.ArtsMartiauxIds == null) ? null : _context.ArtMartial.Where(a => samouraiVM.ArtsMartiauxIds.Contains(a.Id)).ToList();
                    _context.Update(samourai);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamouraiExists(samouraiVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                         throw;
                    }
                }
              
            }
           // return View(samouraiVM);
        }

        // GET: Samourais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samourai = await _context.Samourais.Include(s => s.Arme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samourai == null)
            {
                return NotFound();
            }

            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samourai = await _context.Samourais.FindAsync(id);
            if (samourai != null)
            {
                _context.Samourais.Remove(samourai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamouraiExists(int id)
        {
            return _context.Samourais.Any(e => e.Id == id);
        }
    }
}
