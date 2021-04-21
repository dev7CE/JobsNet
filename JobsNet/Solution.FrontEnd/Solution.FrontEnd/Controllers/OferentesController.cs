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
using System.IO;
using Microsoft.AspNetCore.Http;
using Solution.FrontEnd.DAL;
using Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Controllers
{
    [Authorize(Roles = RoleNames.ROLE_OFERENTE)]
    public class OferentesController : Controller
    {
        private DocumentosRepository _repositoryDocumentos;
        private OferentesRepository _repositoryOferentes;
        private ListaOferentesRepository _repositoryListOferentes;
        
        public OferentesController()
        {
            _repositoryDocumentos = new DocumentosRepository();
            _repositoryOferentes = new OferentesRepository();
            _repositoryListOferentes = new ListaOferentesRepository();
        }

        public async Task<IActionResult> Index(OferentesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == OferentesMessageId.UpdateOferenteSuccess ? "Se ha actualizado tu perfil."
                : message == OferentesMessageId.ChangePasswordSuccess ? "Se ha cambiado tu contraseña."
                : message == OferentesMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            ViewData["PuestosTrabajo"] = await GetPuestosTrabajo();
            return View(await GetOferenteByUserName());
        }
        //
        // GET: Oferentes/Edit
        public async Task<IActionResult> Edit(OferentesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == OferentesMessageId.ResumeDeletedSuccess ? "Se ha eliminado el currículo."
                : message == OferentesMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            ViewData["Resume"] = await GetDocumento();
            return View(await GetOferenteByUserName());
        }
        // 
        // POST: Oferentes/Edit
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("IdOferente,Nombre,Apellido1,Apellido2,Telefono,UrlCurriculo,UrlFoto,UserName")] data.Oferentes model)
        {
            data.Oferentes oferente = await GetOferenteByUserName();
            if (model.IdOferente != oferente.IdOferente)
            return NotFound();

            model.UserName = oferente.UserName;
            if (!ModelState.IsValid)
            return View(model);

            if(await _repositoryOferentes.UpdateOferente(model))
            return RedirectToAction(nameof(Index), new { Message = OferentesMessageId.UpdateOferenteSuccess });

            return RedirectToAction(nameof(Index), new { Message = OferentesMessageId.Error });
        }
        //
        // POST: Oferentes/Add (Resume)
        [HttpPost]
        public async Task<ActionResult> Add(data.Oferentes model, IFormFile files)
        {
            if (files != null && model.IdOferente != 0)
            { 
                var memoryStream = new MemoryStream();
                files.CopyTo(memoryStream);
                if (memoryStream.Length < 2097152)
                {
                    if(!files.FileName.Split('.').LastOrDefault().ToLower().Equals("pdf"))
                    return BadRequest("Icorrect File Type");
                              
                    if(await _repositoryDocumentos.AddResume(new data.Documentos
                    {
                        UserName = User.Identity.Name,
                        Guid = Guid.NewGuid().ToString(),
                        FileContent = memoryStream.ToArray(),
                        Type = files.FileName.Split('.').LastOrDefault().ToLower()
                    }))
                    return Ok((await GetDocumento()).Guid);
                    //byte[] Evidencia = memoryStream.ToArray();
                    //string Tipo = file.ContentType;
                }
                return BadRequest("Error adding file");
            }
            else
            {
                return BadRequest("File null"); // Oops!
            }
        }
        // 
        // DELETE: Oferentes/Revert
        [HttpDelete]
        public async Task<IActionResult> Revert()
        {
            // Read the request body
            string content = "";
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                content = await sr.ReadToEndAsync();
            }
            if(string.IsNullOrEmpty(content))
            return BadRequest("Error encountered on server. Message: Null request");
            
            try
            {
                data.Documentos doc = await GetDocumento();
                if (!content.Equals((doc.Guid)))
                return BadRequest("Error encountered on server. Message: Incorrect Params");
                
                if(!await _repositoryDocumentos.DeleteResume(doc.Id))
                return BadRequest("Error encountered on server. Message: Could not delete file");
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Error encountered on server. Message:'{0}' when writing an object", e.Message));
            }
        }
        // 
        // POST: Empresas/model
        [HttpPost]
        public async Task<IActionResult> RemoveResume(string guid)
        {
            data.Documentos doc = await GetDocumento();
            if (!guid.Equals((doc.Guid)))
            return Json(new { response = "invalid" });   

            if(await _repositoryDocumentos.DeleteResume(doc.Id))
            return Json(new { response = "deleted" });

            return Json(new { response = "error" });
        }
        #region Helpers
        private async Task<data.Oferentes> GetOferenteByUserName ()
        {
            string userName = User.Identity.Name; 
            return (await _repositoryOferentes.GetOferentes())
                .SingleOrDefault(o => o.UserName.Equals(userName));
        }
        public async Task<IEnumerable<data.PuestosTrabajo>> GetPuestosTrabajo()
        {
            List<data.PuestosTrabajo> puestosTrabajo = new List<data.PuestosTrabajo>();
            
            IEnumerable<data.ListaOferentes> aux = 
                (await _repositoryListOferentes.GetListaOferentes())
                .Where(lo => lo.Oferente.UserName.Equals(User.Identity.Name));
                
            foreach (var itemOferente in aux)
            puestosTrabajo.Add(itemOferente.PuestoTrabajo);
            
            return puestosTrabajo;
        }
        public async Task<data.Documentos> GetDocumento()
        {
            return (await _repositoryDocumentos.GetDocumentos())
                .SingleOrDefault(d => d.UserName.Equals(User.Identity.Name));
        }
        public enum OferentesMessageId
        {
            UpdateOferenteSuccess,
            ResumeDeletedSuccess,
            ChangePasswordSuccess,
            Error
        }
        #endregion
    }
}
