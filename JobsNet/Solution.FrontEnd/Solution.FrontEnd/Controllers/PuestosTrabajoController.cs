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

namespace Solution.FrontEnd.Controllers
{
    public class PuestosTrabajoController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public PuestosTrabajoController() { }
        //
        // GET: PuestosTrabajo
        public async Task<IActionResult> Index(ControllerMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ControllerMessageId.AddPuestoTrabajoSuccess ? "Se ha agregado el puesto."
                : message == ControllerMessageId.Error ? "Ha ocurrido un error."
                : "";

            List<data.PuestosTrabajo> aux = new List<data.PuestosTrabajo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/PuestosTrabajo");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.PuestosTrabajo>>(auxres);
                }
            }
            return View(GetByUserName(aux));
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
            puestosTrabajo.IdEmpresa = GetEmpresaByUserName().Result.IdEmpresa;
            if (ModelState.IsValid)
            {
                if(await CreatePuesto(puestosTrabajo))
                return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.AddPuestoTrabajoSuccess });

                return RedirectToAction(nameof(Index), new { Message = ControllerMessageId.Error });
            }
            return View(puestosTrabajo);
        }
        #region Helpers
        private IEnumerable<data.PuestosTrabajo> GetByUserName (IEnumerable<data.PuestosTrabajo> list)
        {
            return list.Where(e => 
                e.Empresa.UserName.Equals(User.Identity.Name))
                .OrderByDescending(e => e.FechaCierre)
                .ToList();
        }

        private async Task<data.Empresas> GetEmpresaByUserName()
        {
            // Here is the code
            IEnumerable<data.Empresas> e = await GetEmpresas();
            return e.SingleOrDefault(e => 
                e.UserName.Equals(User.Identity.Name)
            );
        }
        private async Task<IEnumerable<data.Empresas>> GetEmpresas ()
        {
            List<data.Empresas> aux = new List<data.Empresas>();
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
                    aux = JsonConvert.DeserializeObject<List<data.Empresas>>(auxres);
                }
            }
            return aux;
        }

        private async Task<bool> CreatePuesto(data.PuestosTrabajo model)
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
                    .PostAsync("api/PuestosTrabajo", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public enum ControllerMessageId
        {
            AddPuestoTrabajoSuccess,
            Error
        }
        #endregion
    }
}
