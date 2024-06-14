using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPDojoWeb;
using TPDojoWeb.BO;

namespace TPDojoWeb.Controllers
{
    public class ArmesController : Controller
    {
        private readonly HelloWorldDbContext _context;
        private string BASE_URL = "https://localhost:7153/";

        

        public ArmesController(HelloWorldDbContext context)
        {
            _context = context;
        }

        // GET: Armes
        //[HttpGet("/api/Armes")]
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Armes.ToListAsync());
          
            // to test use api
            HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(30);
            HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:7221/api/Armes");
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            _ = response.EnsureSuccessStatusCode();
            List<Arme> weapons = await response.Content.ReadFromJsonAsync<List<Arme>>();

            return this.View(weapons);
        }
     
        // GET: Armes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arme = await _context.Armes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arme == null)
            {
                return NotFound();
            }

            return View(arme);
        }

        // GET: Armes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Degat")] Arme arme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arme);
        }

        // GET: Armes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arme = await _context.Armes.FindAsync(id);
            if (arme == null)
            {
                return NotFound();
            }
            return View(arme);
        }

        // POST: Armes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Degat")] Arme arme)
        {
            if (id != arme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmeExists(arme.Id))
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
            return View(arme);
        }

        // GET: Armes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arme = await _context.Armes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arme == null)
            {
                return NotFound();
            }

            return View(arme);
        }

        // POST: Armes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arme = await _context.Armes.FindAsync(id);
            //if (arme != null)
            //{

            //    if (_context.IsWeaponAssignedToSamurai(arme.Id))
            //    {
            //        Samourai? samourai = _context.Samourais.Where(s => s.Arme.Id == arme.Id).SingleOrDefault();
            //        Samourai porteur = samourai;
            //        throw new Exception($"Cette arme est assignée à un samouraï : {porteur.Nom}. Impossible de la supprimer.");
            //    }
            //    _context.Armes.Remove(arme);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            if (arme is null)
            {
                return NotFound();
            }
            try
            {

                if (_context.IsWeaponAssignedToSamurai(arme.Id))
                {
                    Samourai? samourai = _context.Samourais.Where(s => s.Arme.Id == arme.Id).SingleOrDefault();
                    Samourai porteur = samourai;
                    throw new Exception($"Cette arme est assignée à un samouraï : {porteur.Nom}. Impossible de la supprimer.");
                }
                _context.Armes.Remove(arme);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(arme);
            }
        }

        private bool ArmeExists(int id)
        {
            return _context.Armes.Any(e => e.Id == id);
        }
    }
}
