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
    public class OferentesController : Controller
    {
        private readonly JobsNetDbContext _context;

        public OferentesController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: Oferentes
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.Oferentes.Include(o => o.UserNameNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: Oferentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oferentes = await _context.Oferentes
                .Include(o => o.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.IdOferente == id);
            if (oferentes == null)
            {
                return NotFound();
            }

            return View(oferentes);
        }

        // GET: Oferentes/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName");
            return View();
        }

        // POST: Oferentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOferente,Nombre,Apellido1,Apellido2,Telefono,UrlCurriculo,UrlFoto,UserName")] Oferentes oferentes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oferentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", oferentes.UserName);
            return View(oferentes);
        }

        // GET: Oferentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oferentes = await _context.Oferentes.FindAsync(id);
            if (oferentes == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", oferentes.UserName);
            return View(oferentes);
        }

        // POST: Oferentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOferente,Nombre,Apellido1,Apellido2,Telefono,UrlCurriculo,UrlFoto,UserName")] Oferentes oferentes)
        {
            if (id != oferentes.IdOferente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oferentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OferentesExists(oferentes.IdOferente))
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
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", oferentes.UserName);
            return View(oferentes);
        }

        // GET: Oferentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oferentes = await _context.Oferentes
                .Include(o => o.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.IdOferente == id);
            if (oferentes == null)
            {
                return NotFound();
            }

            return View(oferentes);
        }

        // POST: Oferentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oferentes = await _context.Oferentes.FindAsync(id);
            _context.Oferentes.Remove(oferentes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OferentesExists(int id)
        {
            return _context.Oferentes.Any(e => e.IdOferente == id);
        }
    }
}
