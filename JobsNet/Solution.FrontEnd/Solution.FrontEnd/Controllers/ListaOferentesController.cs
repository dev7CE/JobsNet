using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using data = Solution.FrontEnd.Models;
using System.Text;
using Solution.FrontEnd.DAL;
using Microsoft.AspNetCore.Authorization;
using Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class ListaOferentesController : Controller
    {
        private ListaOferentesRepository _repositoryListaOferentes;
        private PuestosTrabajoRepository _repositoryPuestosTrabajo;
        private DocumentosRepository _repositoryDocumentos;
        private OferentesRepository _repositoryOferentes;
        public ListaOferentesController()
        {
            _repositoryListaOferentes = new ListaOferentesRepository();
            _repositoryPuestosTrabajo = new PuestosTrabajoRepository();
            _repositoryDocumentos = new DocumentosRepository();
            _repositoryOferentes = new OferentesRepository();
        }

        //
        // GET: ListaOferentes/Index/5
        [Authorize(Roles=RoleNames.ROLE_EMPLEADOR)]
        public async Task<IActionResult> Index(int idPuesto, ControllerMessageId? message = null)
        {
            ViewData["StatusMessage"] = 
                message == ControllerMessageId.UpdateItemOferenteSuccess ? "Se ha actualizado el estado del candidato."
                : message == ControllerMessageId.Error ? "Ha ocurrido un error con la solicitud."
                : "";

            ViewData["puesto"] = await _repositoryPuestosTrabajo.GetPuestoTrabajo(idPuesto); 
            return View(await GetByPuesto(idPuesto));
        }
        //
        //// POST: ListaOferentes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles=RoleNames.ROLE_EMPLEADOR)]
        public async Task<IActionResult> SetDiscart(int idOferente, int idPuesto)
        {
            data.ListaOferentes oferente = await _repositoryListaOferentes.GetListaOferentesByIds(idOferente, idPuesto);
            if (oferente == null)
            return NotFound();
            
            oferente.Descartado = !oferente.Descartado;

            if(await _repositoryListaOferentes.UpdateItemListaOferentes(idOferente, idPuesto, oferente))
            return RedirectToAction(nameof(Index), new { idPuesto = idPuesto, Message = ControllerMessageId.UpdateItemOferenteSuccess });

            return View();
        }
        // 
        // POST: ListaOferentes/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles=RoleNames.ROLE_OFERENTE)]
        public async Task<IActionResult> Submit (int idOferente, int idPuesto)
        {
            if(await _repositoryListaOferentes.InsertItemListaOferentes(new data.ListaOferentes 
            {
                IdOferente = idOferente, 
                IdPuesto = idPuesto
            }))
            return RedirectToAction("Details", "PuestosTrabajo", new { id = idPuesto, Message = ControllerMessageId.PostulateOferenteSuccess });
            
            return RedirectToAction("Details", "PuestosTrabajo",new { id = idPuesto, Message = ControllerMessageId.Error });

        }
        // 
        // GET: Open Resume
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Resume(int id)
        {
            data.Oferentes oferente = await _repositoryOferentes.GetOferenteById(id);

            if (oferente == null)
            return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.Error });

            data.Documentos documento = await GetResumeByUserName(oferente.UserName);
            
            if (documento == null)
            return NotFound();

            // Descarga el archivo
            //return File(documento.FileContent, "application/pdf", 
            //    $"Resume_{documento.Id}.{documento.Type}");
            // Abre En nueva ventana
            return File(documento.FileContent, "application/pdf");
        }
        #region Helpers
        private async Task<IEnumerable<data.ListaOferentes>> GetByPuesto(int idPuesto)
        {
            return (await _repositoryListaOferentes.GetListaOferentes())
                .Where(lo => lo.IdPuesto == idPuesto);
        }
        private async Task<data.Documentos>GetResumeByUserName (string userName)
        {
            return (await _repositoryDocumentos.GetDocumentos())
                .SingleOrDefault(d => d.UserName.Equals(userName));
        }
        public enum ControllerMessageId
        {
            UpdateItemOferenteSuccess,
            PostulateOferenteSuccess,
            Error
        }
        #endregion
    }
}
