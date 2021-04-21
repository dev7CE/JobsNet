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
    public class ListaOferentesRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        
        public async Task<IEnumerable<data.ListaOferentes>> GetListaOferentes ()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/ListaOferentes");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.ListaOferentes>>(auxres);
                }
                return null;
            }
        }
        public async Task<data.ListaOferentes> GetListaOferentesByIds (int idOferente, int idPuesto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"api/ListaOferentes/{idOferente}/{idPuesto}");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<data.ListaOferentes>(auxres);
                }
                return null;
            }
        }
        public async Task<bool> UpdateItemListaOferentes(int idOferente, int idPuesto, data.ListaOferentes model)
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
                    .PutAsync("api/ListaOferentes/"+idOferente+"/"+idPuesto, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<bool> InsertItemListaOferentes(data.ListaOferentes model)
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
                    .PostAsync("api/ListaOferentes", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
    }
}
