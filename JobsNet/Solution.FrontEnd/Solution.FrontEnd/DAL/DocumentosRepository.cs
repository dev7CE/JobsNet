using data = Solution.FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Solution.FrontEnd.DAL
{
    public class DocumentosRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        public async Task<IEnumerable<data.Documentos>> GetDocumentos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Documentos");
        
                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<IEnumerable<data.Documentos>>(auxres);
                }
                return null;
            }
        }
    }
}
