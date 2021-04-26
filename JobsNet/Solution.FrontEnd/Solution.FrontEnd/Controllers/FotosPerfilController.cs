using data = Solution.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solution.FrontEnd.DAL;
using Solution.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Solution.FrontEnd.Controllers
{
    public class FotosPerfilController : Controller
    {
        private FotosPerfilRepository _repositoryFotosPerfil;

        public FotosPerfilController()
        {
            _repositoryFotosPerfil = new FotosPerfilRepository();
        }
        //
        // POST: FotosPerfil/Create 
        [HttpPost]
        public async Task<ActionResult> Create(data.FotosPerfil model, IFormFile files)
        {
            if (files != null && model.UserName.Equals(User.Identity.Name))
            { 
                var memoryStream = new MemoryStream();
                files.CopyTo(memoryStream);
                if (memoryStream.Length < 2097152)
                {
                    string fileExtension = files.FileName.Split('.').LastOrDefault().ToLower();
                    if(!fileExtension.Equals("png") && !fileExtension.Equals("jpeg")
                        && !fileExtension.Equals("jpg"))
                    return BadRequest("Icorrect File Type");

                    model.Guid = Guid.NewGuid().ToString();
                    model.FileContent = memoryStream.ToArray();
                    model.Type = files.FileName.Split('.').LastOrDefault().ToLower();

                    if(await _repositoryFotosPerfil.AddProfilePicture(model))
                    return Ok((await GetProfilePicture()).Guid);
                }
                return BadRequest("Error adding file");
            }
            else
            {
                return BadRequest("File null"); // Oops!
            }
        }
        // 
        // DELETE: FotosPerfl/Revert
        [HttpDelete]
        public async Task<IActionResult> Revert()
        {
            // Read the request body
            string content = "";
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                content = await sr.ReadToEndAsync();
            }
            if(string.IsNullOrEmpty(content))
            return BadRequest("Error encountered on server. Message: Null request");
            
            try
            {
                data.FotosPerfil pic = await GetProfilePicture();
                if (!content.Equals((pic.Guid)))
                return BadRequest("Error encountered on server. Message: Incorrect Params");
                
                if(!await _repositoryFotosPerfil.DeleteProfilePicture(pic.Id))
                return BadRequest("Error encountered on server. Message: Could not delete file");
                
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Error encountered on server. Message:'{0}' when writing an object", e.Message));
            }
        }
        // 
        // POST: FotosPerfil/RemoveProfilePicture
        [HttpPost]
        public async Task<IActionResult> RemoveProfilePicture(string guid)
        {
            data.FotosPerfil pic = await GetProfilePicture();
            if (!guid.Equals((pic.Guid)))
            return Json(new { response = "invalid" });   
        
            if(await _repositoryFotosPerfil.DeleteProfilePicture(pic.Id))
            return Json(new { response = "deleted" });
        
            return Json(new { response = "error" });
        }
        #region Helpers
        private async Task<data.FotosPerfil> GetProfilePicture()
        {
            return (await _repositoryFotosPerfil.GetFotosPerfil())
                .SingleOrDefault(f => f.UserName.Equals(User.Identity.Name));
        }
        #endregion
    }
}
