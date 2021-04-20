using data = Solution.FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Solution.FrontEnd.DAL
{
    public class ProvinciasRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        public async Task<IEnumerable<data.Provincias>> GetProvincias ()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Provincias");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Provincias>>(auxres);
                }
                return null;
            }
        }
        //public async Task<data.Cantones> GetCantonesByIdProvincia(int id)
        //{
        //    return (await GetProvincias()).Where (p => p.)
        //}
    }
}
