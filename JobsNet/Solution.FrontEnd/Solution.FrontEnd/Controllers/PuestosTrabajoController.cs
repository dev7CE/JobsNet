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

namespace Solution.FrontEnd.Controllers
{
    public class PuestosTrabajoController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public PuestosTrabajoController() { }
        //
        // GET: PuestosTrabajo
        public async Task<IActionResult> Index()
        {
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
        #region Helpers
        private IEnumerable<data.PuestosTrabajo> GetByUserName (IEnumerable<data.PuestosTrabajo> list)
        {
            return list.Where(e => 
                e.Empresa.UserName.Equals(User.Identity.Name))
                .OrderByDescending(e => e.FechaCierre)
                .ToList();
        }
        #endregion
    }
}
