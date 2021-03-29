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
    public class ProvinciasController : Controller
    {
        private readonly JobsNetDbContext _context;

        public ProvinciasController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: Provincias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provincias.ToListAsync());
        }

        // GET: Provincias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias
                .FirstOrDefaultAsync(m => m.IdProvincia == id);
            if (provincias == null)
            {
                return NotFound();
            }

            return View(provincias);
        }

        // GET: Provincias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvincia,NombreProvincia")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provincias);
        }

        // GET: Provincias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias.FindAsync(id);
            if (provincias == null)
            {
                return NotFound();
            }
            return View(provincias);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvincia,NombreProvincia")] Provincias provincias)
        {
            if (id != provincias.IdProvincia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciasExists(provincias.IdProvincia))
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
            return View(provincias);
        }

        // GET: Provincias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias
                .FirstOrDefaultAsync(m => m.IdProvincia == id);
            if (provincias == null)
            {
                return NotFound();
            }

            return View(provincias);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provincias = await _context.Provincias.FindAsync(id);
            _context.Provincias.Remove(provincias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinciasExists(int id)
        {
            return _context.Provincias.Any(e => e.IdProvincia == id);
        }
    }
}
