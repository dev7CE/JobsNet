using data = Solution.FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace Solution.FrontEnd.DAL
{
    public class EmpresasRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        
        public async Task<data.Empresas> GetEmpresaByUserName (string userName)
        {
            if(string.IsNullOrEmpty(userName))
            return null;
        
            return (await GetEmpresas())
                .SingleOrDefault(e => e.UserName.Equals(userName));
        }
        public async Task<bool> UpdateEmpresa(data.Empresas model)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(model), 
                    Encoding.UTF8, 
                    "application/json"
                );

                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client
                    .PutAsync("api/Empresas/"+model.IdEmpresa, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<IEnumerable<data.Empresas>> GetEmpresas()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Empresas");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Empresas>>(auxres);
                }
                return null;
            }
        }
        public async Task<bool> CreateEmpresa(data.Empresas model)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(
                    JsonConvert.SerializeObject(model), 
                    Encoding.UTF8, 
                    "application/json"
                );

                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client
                    .PostAsync("api/Empresas", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        #region Private Methods
        #endregion
    }
}
