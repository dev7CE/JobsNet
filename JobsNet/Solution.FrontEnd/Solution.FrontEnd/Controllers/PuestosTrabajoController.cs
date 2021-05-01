using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using data = Solution.FrontEnd.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Solution.FrontEnd.Models;
using Microsoft.Extensions.Options;
using System.Text;
using Solution.FrontEnd.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Solution.FrontEnd.Controllers
{
    [Authorize(Roles=RoleNames.ROLE_EMPLEADOR)]
    public class PuestosTrabajoController : Controller
    {
        private EmpresasRepository _repositoryEmpresas;
        private PuestosTrabajoRepository _repositoryPuestosTrabajo;
        
        public PuestosTrabajoController()
        {
            _repositoryEmpresas = new EmpresasRepository();
            _repositoryPuestosTrabajo = new PuestosTrabajoRepository();
        }
        //
        // GET: PuestosTrabajo
        public async Task<IActionResult> Index(ControllerMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ControllerMessageId.AddPuestoTrabajoSuccess ? "Se ha agregado el puesto."
                : message == ControllerMessageId.UpdatePuestoTrabajoSuccess ? "Se ha actualizado el puesto."
                : message == ControllerMessageId.Error ? "Ha ocurrido un error."
                : "";
            
            return View(await GetPuestosTrabajoByUserName());
        }
        //
        // GET: PuestosTrabajo/Create
        public IActionResult Create()
        {
            //ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "NombreEmpresa");
            return View();
        }
        //
        // POST: PuestosTrabajo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPuesto,Titulo,Descripcion,Requisitos,FechaCierre")] PuestosTrabajo puestosTrabajo)
        {
            puestosTrabajo.IdEmpresa = (await GetEmpresaByUserName()).IdEmpresa;
            if (ModelState.IsValid)
            {
                if(await _repositoryPuestosTrabajo.CreatePuesto(puestosTrabajo))
                return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.AddPuestoTrabajoSuccess });

                return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.Error });
            }
            return View(puestosTrabajo);
        }
        //
        // GET: PuestosTrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id = 0)
        {
            if (id == null && id <= 0)
            return NotFound();
            
            var puestosTrabajo = 
                await _repositoryPuestosTrabajo.GetPuestoTrabajo((int)id);
            
            if (puestosTrabajo == null)
            return NotFound();

            return View(puestosTrabajo);
        }
        //
        // POST: PuestosTrabajo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdPuesto, int IdEmpresa, [Bind("IdPuesto,IdEmpresa,Titulo,Descripcion,Requisitos,FechaPublicacion,FechaCierre")] PuestosTrabajo puestosTrabajo)
        {
            if (IdPuesto != puestosTrabajo.IdPuesto)    
            return NotFound();

            if (!ModelState.IsValid)
            return View(puestosTrabajo);
            
            if (await _repositoryPuestosTrabajo.UpdatePuesto(IdPuesto, puestosTrabajo))
            return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.UpdatePuestoTrabajoSuccess });
            
            return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.Error });
        }
        //
        // POST: PuestosTrabajo/StopReceptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StopReceptions(int id)
        {
            data.PuestosTrabajo puestoTrabajo = 
                await _repositoryPuestosTrabajo.GetPuestoTrabajo(id); 
            if (puestoTrabajo == null)    
            return NotFound();

            // Update Receptions End Date
            puestoTrabajo.FechaCierre = System.DateTime.Now.AddDays(-1).Date;

            if (await _repositoryPuestosTrabajo.UpdatePuesto(id, puestoTrabajo))
            return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.UpdatePuestoTrabajoSuccess });
            
            return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.Error });
        }
        // Oferentes
        //
        // GET: PuestosTrabajo/All
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View(await _repositoryPuestosTrabajo.GetPuestosTrabajo());
        }
        //
        // GET: PuestosTrabajo/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id, ControllerMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ControllerMessageId.PostulateOferenteSuccess ? "Te has postulado a esta oferta."
                : "";

            if (id == null)
            return NotFound();

            data.PuestosTrabajo puestoTrabajo = await _repositoryPuestosTrabajo.GetPuestoTrabajo((int) id);

            if (puestoTrabajo == null)
            return NotFound();

            return View(puestoTrabajo);
        }
        #region Helpers
        private async Task<IEnumerable<data.PuestosTrabajo>> GetPuestosTrabajoByUserName ()
        {
            return (await _repositoryPuestosTrabajo.GetPuestosTrabajo())
                .Where(e => e.Empresa.UserName.Equals(User.Identity.Name))
                .OrderByDescending(e => e.FechaCierre)
                .ToList();
        }
        private async Task<data.Empresas> GetEmpresaByUserName()
        {
            IEnumerable<data.Empresas> e = await _repositoryEmpresas.GetEmpresas();
            return e.SingleOrDefault(e => e.UserName.Equals(User.Identity.Name));
        }
        public enum ControllerMessageId
        {
            AddPuestoTrabajoSuccess,
            UpdatePuestoTrabajoSuccess,
            PostulateOferenteSuccess,
            Error
        }
        #endregion
    }
}
