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

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public EmpresasController() { }

        public async Task<IActionResult> Index(EmpresasMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == EmpresasMessageId.UpdateEmpresaSuccess ? "Se ha actualizado tu Empresa."
                : message == EmpresasMessageId.ChangePasswordSuccess ? "Se ha cambiado tu contraseña."
                : message == EmpresasMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            return View(await GetEmpresaByUserName());
        }
        //
        // GET: Empresas/Edit
        public async Task<IActionResult> Edit()
        {
            return View(await GetEmpresaByUserName());
        }
        // 
        // POST: Empresas/model
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("IdEmpresa,NombreEmpresa,Descripcion,Telefono,IdCanton,UserName")] data.Empresas model)
        {
            data.Empresas empresa = await GetEmpresaByUserName();
            if (model.IdEmpresa != empresa.IdEmpresa)
            return NotFound();

            model.UserName = empresa.UserName;
            if (!ModelState.IsValid)
            return View(model);

            if(await UpdateEmpresa(model))
            return RedirectToAction(nameof(Index), new { Message = EmpresasMessageId.UpdateEmpresaSuccess });

            return RedirectToAction(nameof(Index), new { Message = EmpresasMessageId.Error });
        }
        // 
        // GET: Empresas/Provincias
        public async Task<IActionResult> Provincias()
        {
            return Json(await GetProvincias());
        }
        // 
        // GET: Empresas/Cantones/Provincia.Id
        public async Task<IActionResult> CantonesByProvincia(int idProvincia)
        {
            IEnumerable<data.Cantones> cantones = await GetCantones(idProvincia);
            if(cantones.Count() > 0) 
            return Json(cantones);

            return NotFound();
        }
        #region Helpers
        public async Task<data.Empresas> GetEmpresaByUserName ()
        {
            string userName = User.Identity.Name; 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Empresas");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Empresas>>(auxres)
                        .SingleOrDefault(o => o.UserName.Equals(userName));
                }
                return null;
            }
        }
        public async Task<IEnumerable<data.Provincias>> GetProvincias ()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Provincias");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Provincias>>(auxres);
                }
                return null;
            }
        }
        public async Task<IEnumerable<data.Cantones>> GetCantones (int idProvincia)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Cantones");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Cantones>>(auxres)
                        .Where(c => c.IdProvincia == idProvincia);
                }
                return null;
            }
        }
        private async Task<bool> UpdateEmpresa(data.Empresas model)
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
                    .PutAsync("api/Empresas/"+model.IdEmpresa, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public enum EmpresasMessageId
        {
            UpdateEmpresaSuccess,
            ChangePasswordSuccess,
            Error
        }
        #endregion
    }
}
