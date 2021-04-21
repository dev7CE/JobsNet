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
    public class OferentesRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        
        public async Task<IEnumerable<data.Oferentes>> GetOferentes ()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Oferentes");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Oferentes>>(auxres);
                }
                return null;
            }
        }
        public async Task<data.Oferentes> GetOferenteById (int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"api/Oferentes/{id}");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<data.Oferentes>(auxres);
                }
                return null;
            }
        }
        public async Task<bool> UpdateOferente(data.Oferentes model)
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
                    .PutAsync("api/Oferentes/"+model.IdOferente, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<bool> CreateOferente(data.Oferentes model)
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
                    .PostAsync("api/Oferentes", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
    }
}
