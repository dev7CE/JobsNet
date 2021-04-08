using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using data = Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Controllers
{
    public class ListaOferentesController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public ListaOferentesController() { }
        //
        // GET: ListaOferentes/Index/5
        public async Task<IActionResult> Index(int idPuesto)
        {
            ViewData["puesto"] = await GetPuesto(idPuesto); 
            return View(await GetByPuesto(idPuesto));
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
        #endregion
    }
}
