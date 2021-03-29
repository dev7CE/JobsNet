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
    public class ListaOferentesController : Controller
    {
        private readonly JobsNetDbContext _context;

        public ListaOferentesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: ListaOferentes
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.ListaOferentes.Include(l => l.IdOferenteNavigation).Include(l => l.IdPuestoNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: ListaOferentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaOferentes = await _context.ListaOferentes
                .Include(l => l.IdOferenteNavigation)
                .Include(l => l.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdOferente == id);
            if (listaOferentes == null)
            {
                return NotFound();
            }

            return View(listaOferentes);
        }

        // GET: ListaOferentes/Create
        public IActionResult Create()
        {
            ViewData["IdOferente"] = new SelectList(_context.Oferentes, "IdOferente", "Nombre");
            ViewData["IdPuesto"] = new SelectList(_context.PuestosTrabajo, "IdPuesto", "Titulo");
            return View();
        }

        // POST: ListaOferentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOferente,IdPuesto,Descartado")] ListaOferentes listaOferentes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaOferentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOferente"] = new SelectList(_context.Oferentes, "IdOferente", "Nombre", listaOferentes.IdOferente);
            ViewData["IdPuesto"] = new SelectList(_context.PuestosTrabajo, "IdPuesto", "Titulo", listaOferentes.IdPuesto);
            return View(listaOferentes);
        }

        // GET: ListaOferentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaOferentes = await _context.ListaOferentes.FindAsync(id);
            if (listaOferentes == null)
            {
                return NotFound();
            }
            ViewData["IdOferente"] = new SelectList(_context.Oferentes, "IdOferente", "Nombre", listaOferentes.IdOferente);
            ViewData["IdPuesto"] = new SelectList(_context.PuestosTrabajo, "IdPuesto", "Titulo", listaOferentes.IdPuesto);
            return View(listaOferentes);
        }

        // POST: ListaOferentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOferente,IdPuesto,Descartado")] ListaOferentes listaOferentes)
        {
            if (id != listaOferentes.IdOferente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaOferentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaOferentesExists(listaOferentes.IdOferente))
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
            ViewData["IdOferente"] = new SelectList(_context.Oferentes, "IdOferente", "Nombre", listaOferentes.IdOferente);
            ViewData["IdPuesto"] = new SelectList(_context.PuestosTrabajo, "IdPuesto", "Titulo", listaOferentes.IdPuesto);
            return View(listaOferentes);
        }

        // GET: ListaOferentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaOferentes = await _context.ListaOferentes
                .Include(l => l.IdOferenteNavigation)
                .Include(l => l.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdOferente == id);
            if (listaOferentes == null)
            {
                return NotFound();
            }

            return View(listaOferentes);
        }

        // POST: ListaOferentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listaOferentes = await _context.ListaOferentes.FindAsync(id);
            _context.ListaOferentes.Remove(listaOferentes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaOferentesExists(int id)
        {
            return _context.ListaOferentes.Any(e => e.IdOferente == id);
        }
    }
}
