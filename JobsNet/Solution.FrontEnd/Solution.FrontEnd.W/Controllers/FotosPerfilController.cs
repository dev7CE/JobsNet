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
    public class FotosPerfilController : Controller
    {
        private readonly JobsNetDbContext _context;

        public FotosPerfilController(JobsNetDbContext context)
        {
            _context = context;
        }

        // GET: FotosPerfil
        public async Task<IActionResult> Index()
        {
            var jobsNetDbContext = _context.FotosPerfil.Include(f => f.UserNameNavigation);
            return View(await jobsNetDbContext.ToListAsync());
        }

        // GET: FotosPerfil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotosPerfil = await _context.FotosPerfil
                .Include(f => f.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotosPerfil == null)
            {
                return NotFound();
            }

            return View(fotosPerfil);
        }

        // GET: FotosPerfil/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName");
            return View();
        }

        // POST: FotosPerfil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,FileContent,Type")] FotosPerfil fotosPerfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotosPerfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", fotosPerfil.UserName);
            return View(fotosPerfil);
        }

        // GET: FotosPerfil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotosPerfil = await _context.FotosPerfil.FindAsync(id);
            if (fotosPerfil == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", fotosPerfil.UserName);
            return View(fotosPerfil);
        }

        // POST: FotosPerfil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,FileContent,Type")] FotosPerfil fotosPerfil)
        {
            if (id != fotosPerfil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotosPerfil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotosPerfilExists(fotosPerfil.Id))
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
            ViewData["UserName"] = new SelectList(_context.Usuarios, "UserName", "UserName", fotosPerfil.UserName);
            return View(fotosPerfil);
        }

        // GET: FotosPerfil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotosPerfil = await _context.FotosPerfil
                .Include(f => f.UserNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotosPerfil == null)
            {
                return NotFound();
            }

            return View(fotosPerfil);
        }

        // POST: FotosPerfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotosPerfil = await _context.FotosPerfil.FindAsync(id);
            _context.FotosPerfil.Remove(fotosPerfil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotosPerfilExists(int id)
        {
            return _context.FotosPerfil.Any(e => e.Id == id);
        }
    }
}
