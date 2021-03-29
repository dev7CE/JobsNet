using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solution.FrontEnd.W.Models;

namespace Solution.FrontEnd.W.Controllers
{
    public class CantonesController : Controller
    {
        private readonly JobsNetDbContext _context;

        public CantonesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: Cantones
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.Cantones.Include(c => c.IdProvinciaNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: Cantones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantones = await _context.Cantones
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (cantones == null)
            {
                return NotFound();
            }

            return View(cantones);
        }

        // GET: Cantones/Create
        public IActionResult Create()
        {
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: Cantones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCanton,NombreCanton,IdProvincia")] Cantones cantones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cantones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "NombreProvincia", cantones.IdProvincia);
            return View(cantones);
        }

        // GET: Cantones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantones = await _context.Cantones.FindAsync(id);
            if (cantones == null)
            {
                return NotFound();
            }
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "NombreProvincia", cantones.IdProvincia);
            return View(cantones);
        }

        // POST: Cantones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCanton,NombreCanton,IdProvincia")] Cantones cantones)
        {
            if (id != cantones.IdCanton)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cantones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantonesExists(cantones.IdCanton))
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
            ViewData["IdProvincia"] = new SelectList(_context.Provincias, "IdProvincia", "NombreProvincia", cantones.IdProvincia);
            return View(cantones);
        }

        // GET: Cantones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cantones = await _context.Cantones
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCanton == id);
            if (cantones == null)
            {
                return NotFound();
            }

            return View(cantones);
        }

        // POST: Cantones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cantones = await _context.Cantones.FindAsync(id);
            _context.Cantones.Remove(cantones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CantonesExists(int id)
        {
            return _context.Cantones.Any(e => e.IdCanton == id);
        }
    }
}
