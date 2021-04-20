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
    public class PuestosTrabajoRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        
        public async Task<IEnumerable<data.PuestosTrabajo>> GetPuestosTrabajo()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/PuestosTrabajo");
            
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.PuestosTrabajo>>(auxres);
                }
                return null;
            }
        }
        public async Task<data.PuestosTrabajo> GetPuestoTrabajo (int id)
        {
            data.PuestosTrabajo aux = new data.PuestosTrabajo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/PuestosTrabajo/"+id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.PuestosTrabajo>(auxres);
                }
            }
            return aux;            
        }
        public async Task<bool> CreatePuesto(data.PuestosTrabajo model)
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
                    .PostAsync("api/PuestosTrabajo", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<bool> UpdatePuesto(int id, data.PuestosTrabajo model)
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
                    .PutAsync("api/PuestosTrabajo/"+id, requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        #region Private Methods
        #endregion
    }
}
