using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;

namespace CodiceFiscale.Controllers
{
    public class ComunisController : Controller
    {
        private readonly ComuniContext _context;

        public ComunisController(ComuniContext context)
        {
            _context = context;
        }

        // GET: Comunis
        public async Task<IActionResult> Index()
        {
              return _context.Comunis != null ? 
                          View(await _context.Comunis.ToListAsync()) :
                          Problem("Entity set 'ComuniContext.Comunis'  is null.");
        }

        // GET: Comunis/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Comunis == null)
            {
                return NotFound();
            }

            var comuni = await _context.Comunis
                .FirstOrDefaultAsync(m => m.Code == id);
            if (comuni == null)
            {
                return NotFound();
            }

            return View(comuni);
        }

        // GET: Comunis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comunis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comune,Regione,Provincia,Sigla,Code")] Comuni comuni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comuni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comuni);
        }

        // GET: Comunis/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Comunis == null)
            {
                return NotFound();
            }

            var comuni = await _context.Comunis.FindAsync(id);
            if (comuni == null)
            {
                return NotFound();
            }
            return View(comuni);
        }

        // POST: Comunis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Comune,Regione,Provincia,Sigla,Code")] Comuni comuni)
        {
            if (id != comuni.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comuni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComuniExists(comuni.Code))
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
            return View(comuni);
        }

        // GET: Comunis/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Comunis == null)
            {
                return NotFound();
            }

            var comuni = await _context.Comunis
                .FirstOrDefaultAsync(m => m.Code == id);
            if (comuni == null)
            {
                return NotFound();
            }

            return View(comuni);
        }

        // POST: Comunis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Comunis == null)
            {
                return Problem("Entity set 'ComuniContext.Comunis'  is null.");
            }
            var comuni = await _context.Comunis.FindAsync(id);
            if (comuni != null)
            {
                _context.Comunis.Remove(comuni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComuniExists(string id)
        {
          return (_context.Comunis?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
