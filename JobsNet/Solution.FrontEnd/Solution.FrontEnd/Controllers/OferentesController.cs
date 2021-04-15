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

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class OferentesController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public OferentesController() { }

        public async Task<IActionResult> Index(OferentesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == OferentesMessageId.UpdateOferenteSuccess ? "Se ha actualizado tu perfil."
                : message == OferentesMessageId.ChangePasswordSuccess ? "Se ha cambiado tu contraseña."
                : message == OferentesMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            ViewData["PuestosTrabajo"] = await GetPuestosTrabajo();
            return View(await GetOferentesByUserName());
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
        // POST: Empresas/model
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("IdOferente,Nombre,Apellido1,Apellido2,Telefono,UrlCurriculo,UrlFoto,UserName")] data.Oferentes model)
        {
            data.Oferentes oferente = await GetOferenteByUserName();
            if (model.IdOferente != oferente.IdOferente)
            return NotFound();

            model.UserName = oferente.UserName;
            if (!ModelState.IsValid)
            return View(model);

            if(await UpdateOferente(model))
            return RedirectToAction(nameof(Index), new { Message = OferentesMessageId.UpdateOferenteSuccess });

            return RedirectToAction(nameof(Index), new { Message = OferentesMessageId.Error });
        }
        //
        // POST: Oferentes/Add
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
                              
                    if(await AddResume(new data.Documentos
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
        public async Task<ActionResult> Revert()
        {
            // Read the request body
            string content = "";
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                content = await sr.ReadToEndAsync();
            }
            
            try
            {
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

            if(await DeleteResume(doc.Id))
            return Json(new { response = "deleted" });

            return Json(new { response = "error" });
        }
        #region Helpers
        public async Task<data.Oferentes> GetOferentesByUserName ()
        {
            string userName = User.Identity.Name; 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Oferentes");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Oferentes>>(auxres)
                        .SingleOrDefault(o => o.UserName.Equals(userName));
                }
                return null;
            }
        }
        public async Task<data.Oferentes> GetOferenteByUserName ()
        {
            string userName = User.Identity.Name; 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Oferentes");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Oferentes>>(auxres)
                        .SingleOrDefault(o => o.UserName.Equals(userName));
                }
                return null;
            }
        }
        private async Task<bool> UpdateOferente(data.Oferentes model)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(model), 
                    Encoding.UTF8, 
                    "application/json"
                );

                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client
                    .PutAsync("api/Oferentes/"+model.IdOferente, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<IEnumerable<data.PuestosTrabajo>> GetPuestosTrabajo()
        {
            List<data.PuestosTrabajo> puestosTrabajo = new List<data.PuestosTrabajo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/ListaOferentes");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    foreach (var itemOferente in JsonConvert
                        .DeserializeObject<List<data.ListaOferentes>>(auxres)
                        .Where(lo => lo.Oferente.UserName.Equals(User.Identity.Name)))
                    {
                        puestosTrabajo.Add(itemOferente.PuestoTrabajo);
                    } 
                }
            }
            return puestosTrabajo;
        }
        public async Task<bool> AddResume (data.Documentos model)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(model), 
                    Encoding.UTF8, 
                    "application/json"
                );

                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PostAsync("api/Documentos", requestContent);
        
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<data.Documentos> GetDocumento()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Documentos");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                   return JsonConvert
                        .DeserializeObject<List<data.Documentos>>(auxres)
                        .SingleOrDefault(d => d.UserName.Equals(User.Identity.Name)); 
                }
            }
            return null;
        }
        public async Task<data.Documentos> GetDocumentos(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Documentos");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert
                        .DeserializeObject<List<data.Documentos>>(auxres)
                        .SingleOrDefault(d => d.Id == id); 
                }
            }
            return null;
        }
        public async Task<bool> DeleteResume (int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.DeleteAsync("api/Documentos/"+id);
        
                return res.IsSuccessStatusCode;
            }
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
