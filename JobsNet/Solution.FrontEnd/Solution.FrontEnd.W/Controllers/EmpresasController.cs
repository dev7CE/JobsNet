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
    public class EmpresasController : Controller
    {
        private readonly JobsNetDbContext _context;

        public EmpresasController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.Empresas.Include(e => e.IdCantonNavigation).Include(e => e.UserNameNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .Include(e => e.IdCantonNavigation)
                .Include(e => e.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpresa == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "NombreCanton");
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresa,NombreEmpresa,Descripcion,Telefono,IdCanton,UserName")] Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "NombreCanton", empresas.IdCanton);
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", empresas.UserName);
            return View(empresas);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas.FindAsync(id);
            if (empresas == null)
            {
                return NotFound();
            }
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "NombreCanton", empresas.IdCanton);
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", empresas.UserName);
            return View(empresas);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpresa,NombreEmpresa,Descripcion,Telefono,IdCanton,UserName")] Empresas empresas)
        {
            if (id != empresas.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresasExists(empresas.IdEmpresa))
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
            ViewData["IdCanton"] = new SelectList(_context.Cantones, "IdCanton", "NombreCanton", empresas.IdCanton);
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", empresas.UserName);
            return View(empresas);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .Include(e => e.IdCantonNavigation)
                .Include(e => e.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpresa == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresas = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresasExists(int id)
        {
            return _context.Empresas.Any(e => e.IdEmpresa == id);
        }
    }
}
