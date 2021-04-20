using data = Solution.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Solution.FrontEnd.DAL;

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        private EmpresasRepository _repositoryEmpresas;
        private ProvinciasRepository _repositoryProvincias;
        private CantonesRepository _repositoryCantones;

        public EmpresasController()
        {
            _repositoryEmpresas = new EmpresasRepository();
            _repositoryProvincias = new ProvinciasRepository();
            _repositoryCantones = new CantonesRepository();
        }

        public async Task<IActionResult> Index(EmpresasMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == EmpresasMessageId.UpdateEmpresaSuccess ? "Se ha actualizado tu Empresa."
                : message == EmpresasMessageId.ChangePasswordSuccess ? "Se ha cambiado tu contraseña."
                : message == EmpresasMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            return View(await _repositoryEmpresas.GetEmpresaByUserName(User.Identity.Name));
        }
        //
        // GET: Empresas/Edit
        public async Task<IActionResult> Edit()
        {
            return View(await _repositoryEmpresas.GetEmpresaByUserName(User.Identity.Name));
        }
        // 
        // POST: Empresas/model
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("IdEmpresa,NombreEmpresa,Descripcion,Telefono,IdCanton,UserName")] data.Empresas model)
        {
            data.Empresas empresa = await _repositoryEmpresas.GetEmpresaByUserName(User.Identity.Name);
            if (model.IdEmpresa != empresa.IdEmpresa)
            return NotFound();

            model.UserName = empresa.UserName;
            if (!ModelState.IsValid)
            return View(model);

            if(await _repositoryEmpresas.UpdateEmpresa(model))
            return RedirectToAction(nameof(Index), new { Message = EmpresasMessageId.UpdateEmpresaSuccess });

            return RedirectToAction(nameof(Index), new { Message = EmpresasMessageId.Error });
        }
        // Json
        // GET: Empresas/GetProvincias
        public async Task<IActionResult> GetProvincias()
        {
            return Json(await _repositoryProvincias.GetProvincias());
        }
        // Json
        // GET: Empresas/Cantones/Provincia.Id
        public async Task<IActionResult> CantonesByProvincia(int idProvincia)
        {
            IEnumerable<data.Cantones> cantones = 
                await _repositoryCantones.GetCantonesByIdProvincia(idProvincia);
            if(cantones.Count() > 0) 
            return Json(cantones);

            return NotFound();
        }
        
        #region Helpers
        public enum EmpresasMessageId
        {
            UpdateEmpresaSuccess,
            ChangePasswordSuccess,
            Error
        }
        #endregion
    }
}
