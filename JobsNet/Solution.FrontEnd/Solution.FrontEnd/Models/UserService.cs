using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using data = Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Models
{
    public class UserService
    {
        private readonly string baseurl = "http://localhost:5000/";
        public UserService() { }
        public async Task<string> NombreOferente (string userName)
        {
            List<data.Oferentes> aux = new List<data.Oferentes>();
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
                    aux = JsonConvert.DeserializeObject<List<data.Oferentes>>(auxres);
                }
            }
            return BuildName(aux, userName);
        }
        
        public async Task<string> NombreEmpresa (string userName)
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
            return BuildName(aux, userName);
        }
        public async Task<data.Oferentes> GetOferenteByUserName (string userName)
        {
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
        public async Task<bool> AlreadyApplied(int idOferente, int idPuesto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client
                    .GetAsync("api/ListaOferentes/"+idOferente+"/"+idPuesto);

                if (res.IsSuccessStatusCode)
                return true;

                return false;
            }
        }
        #region Helpers
        private string BuildName(List<data.Oferentes> oferentes, string userName)
        {
            Oferentes aux = oferentes.SingleOrDefault(o => 
                o.UserName.Equals(userName)
            );
            return $"{aux.Nombre} {aux.Apellido1} {aux.Apellido2}";
        }
        private string BuildName(List<data.Empresas> empresas, string userName)
        {
            Empresas aux = empresas.SingleOrDefault(o => 
                o.UserName.Equals(userName)
            );
            return $"{aux.NombreEmpresa}";
        }
        #endregion
    }
}