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
    public class OferentesController : Controller
    {
        private readonly string baseurl = "http://localhost:5000/";
        public OferentesController() { }

        public async Task<IActionResult> Index(OferentesMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == OferentesMessageId.ChangePasswordSuccess ? "Se ha cambiado tu contraseña."
                : message == OferentesMessageId.Error ? "Ha ocurrido un error con tu solicitud. Inténtalo nuevamente."
                : "";
            
            return View(await GetOferentesByUserName());
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
        public enum OferentesMessageId
        {
            ChangePasswordSuccess,
            Error
        }
        #endregion
    }
}
