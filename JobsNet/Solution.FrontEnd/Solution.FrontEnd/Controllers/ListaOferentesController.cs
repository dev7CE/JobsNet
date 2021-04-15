using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using data = Solution.FrontEnd.Models;
using System.Text;

namespace Solution.FrontEnd.Controllers
{
    public class ListaOferentesController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public ListaOferentesController() { }
        //
        // GET: ListaOferentes/Index/5
        public async Task<IActionResult> Index(int idPuesto, ControllerMessageId? message = null)
        {
            ViewData["StatusMessage"] = 
                message == ControllerMessageId.UpdateItemOferenteSuccess ? "Se ha actualizado el estado del candidato."
                : message == ControllerMessageId.Error ? "Ha ocurrido un error con la solicitud."
                : "";

            ViewData["puesto"] = await GetPuesto(idPuesto); 
            return View(await GetByPuesto(idPuesto));
        }
        //
        //// POST: ListaOferentes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDiscart(int idOferente, int idPuesto)
        {
            data.ListaOferentes oferente = await GetItemListaOferentes(idOferente, idPuesto);
            if (oferente == null)
            return NotFound();
            
            oferente.Descartado = !oferente.Descartado;

            if(await UpdateItemListaOferentes(idOferente, idPuesto, oferente))
            return RedirectToAction(nameof(Index), new { idPuesto = idPuesto, Message = ControllerMessageId.UpdateItemOferenteSuccess });

            return View();
        }
        // 
        // POST: ListaOferentes/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit (int idOferente, int idPuesto)
        {
            //data.ListaOferentes oferente =  await GetItemListaOferentes(idOferente, idPuesto);
            //if (oferente == null)
            if(await InsertItemListaOferentes(new data.ListaOferentes 
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
        public async Task<IActionResult> Resume(int id)
        {
            data.Oferentes oferente = await GetOferenteById(id);
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
            List<data.ListaOferentes> aux = new List<data.ListaOferentes>();
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
                    aux = JsonConvert.DeserializeObject<List<data.ListaOferentes>>(auxres);
                }
            }
            return aux.Where(lo => lo.IdPuesto == idPuesto)
                .ToList();
        }
        private async Task<data.PuestosTrabajo> GetPuesto (int idPuesto)
        {
            data.PuestosTrabajo puesto = new data.PuestosTrabajo();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/PuestosTrabajo/"+idPuesto);
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    puesto = JsonConvert.DeserializeObject<data.PuestosTrabajo>(auxres);
                }
            }

            return puesto;
        }
        private async Task<data.ListaOferentes> GetItemListaOferentes(int idOferente, int idPuesto)
        {
            data.ListaOferentes aux = new data.ListaOferentes();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/ListaOferentes/"+idOferente+"/"+idPuesto);
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.ListaOferentes>(auxres);
                }
            }
            return aux;
        }
        private async Task<bool> UpdateItemListaOferentes(int idOferente, int idPuesto, data.ListaOferentes model)
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
                    .PutAsync("api/ListaOferentes/"+idOferente+"/"+idPuesto, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        private async Task<bool> InsertItemListaOferentes(data.ListaOferentes model)
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
                    .PostAsync("api/ListaOferentes", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        private async Task<data.Oferentes>GetOferenteById (int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Oferentes/"+id);
        
                if (!res.IsSuccessStatusCode)
                return null;
                
                var auxres = res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<data.Oferentes>(auxres);
            }
        }
        private async Task<data.Documentos>GetResumeByUserName (string userName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Documentos");
        
                if (!res.IsSuccessStatusCode)
                return null;

                var auxres = res.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<data.Documentos>>(auxres)
                    .SingleOrDefault(d => d.UserName.Equals(userName));
            }
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
