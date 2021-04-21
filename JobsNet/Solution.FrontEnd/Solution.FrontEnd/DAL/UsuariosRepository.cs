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
    public class UsuariosRepository
    {
        private readonly string _baseurl = "http://localhost:5000/";
        
        public async Task<bool> CreateUsuario(data.Usuarios model)
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
                    .PostAsync("api/Usuarios", requestContent);
                return res.IsSuccessStatusCode;
            }
        }
        public async Task<bool> DeleteUsuario(string userName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers
                        .MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client
                    .DeleteAsync($"api/Usuarios/{userName}");
                return res.IsSuccessStatusCode;
            }
        }
        #region Private Methods
        #endregion
    }
}
