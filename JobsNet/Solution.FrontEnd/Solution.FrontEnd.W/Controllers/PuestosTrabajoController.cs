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
    public class PuestosTrabajoController : Controller
    {
        private readonly JobsNetDbContext _context;

        public PuestosTrabajoController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: PuestosTrabajo
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.PuestosTrabajo.Include(p => p.IdEmpresaNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: PuestosTrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestosTrabajo = await _context.PuestosTrabajo
                .Include(p => p.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdPuesto == id);
            if (puestosTrabajo == null)
            {
                return NotFound();
            }

            return View(puestosTrabajo);
        }

        // GET: PuestosTrabajo/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "NombreEmpresa");
            return View();
        }

        // POST: PuestosTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPuesto,IdEmpresa,Titulo,Descripcion,Requisitos,FechaPublicacion,FechaCierre")] PuestosTrabajo puestosTrabajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puestosTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "NombreEmpresa", puestosTrabajo.IdEmpresa);
            return View(puestosTrabajo);
        }

        // GET: PuestosTrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestosTrabajo = await _context.PuestosTrabajo.FindAsync(id);
            if (puestosTrabajo == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "NombreEmpresa", puestosTrabajo.IdEmpresa);
            return View(puestosTrabajo);
        }

        // POST: PuestosTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPuesto,IdEmpresa,Titulo,Descripcion,Requisitos,FechaPublicacion,FechaCierre")] PuestosTrabajo puestosTrabajo)
        {
            if (id != puestosTrabajo.IdPuesto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puestosTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestosTrabajoExists(puestosTrabajo.IdPuesto))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "NombreEmpresa", puestosTrabajo.IdEmpresa);
            return View(puestosTrabajo);
        }

        // GET: PuestosTrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestosTrabajo = await _context.PuestosTrabajo
                .Include(p => p.IdEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.IdPuesto == id);
            if (puestosTrabajo == null)
            {
                return NotFound();
            }

            return View(puestosTrabajo);
        }

        // POST: PuestosTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puestosTrabajo = await _context.PuestosTrabajo.FindAsync(id);
            _context.PuestosTrabajo.Remove(puestosTrabajo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestosTrabajoExists(int id)
        {
            return _context.PuestosTrabajo.Any(e => e.IdPuesto == id);
        }
    }
}
