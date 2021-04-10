using data = Solution.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public EmpresasController() { }

        public async Task<IActionResult> Index()
        {
            return View(await GetEmpresaByUserName());
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
        #endregion
    }
}
