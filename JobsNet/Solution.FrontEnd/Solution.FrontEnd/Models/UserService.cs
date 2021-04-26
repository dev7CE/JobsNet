using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solution.FrontEnd.DAL;
using data = Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Models
{
    public class UserService
    {
        private EmpresasRepository _repositoryEmpresas;
        private OferentesRepository _repositoryOferentes;
        private ListaOferentesRepository _repositoryListaOferentes;
        private DocumentosRepository _repositoryDocumentos;
        private FotosPerfilRepository _repositoryFotosPerfil;

        public UserService() 
        { 
            _repositoryEmpresas = new EmpresasRepository();
            _repositoryOferentes = new OferentesRepository();
            _repositoryListaOferentes = new ListaOferentesRepository();
            _repositoryDocumentos = new DocumentosRepository();
            _repositoryFotosPerfil = new FotosPerfilRepository();
        }
        
        public async Task<string> NombreOferente (string userName)
        {
            data.Oferentes oferente = (await _repositoryOferentes.GetOferentes())
                .SingleOrDefault(o => o.UserName.Equals(userName));
            return $"{oferente.Nombre} {oferente.Apellido1} {oferente.Apellido2}";
        }
        public async Task<string> NombreEmpresa (string userName)
        {
            data.Empresas empresa = await _repositoryEmpresas.GetEmpresaByUserName(userName);
            return empresa.NombreEmpresa;
        }
        public async Task<data.Oferentes> GetOferenteByUserName (string userName)
        {
            return (await _repositoryOferentes.GetOferentes())
                .SingleOrDefault(o => o.UserName.Equals(userName));
        }
        public async Task<bool> AlreadyApplied(int idOferente, int idPuesto)
        {
            return (await _repositoryListaOferentes
                .GetListaOferentesByIds(idOferente, idPuesto))
                != null; 
        }
        public async Task<bool> HasResume(int idOferente)
        {
            data.Oferentes oferente = 
                await _repositoryOferentes.GetOferenteById(idOferente);

            if(oferente == null)
            return false;

            return (await GetDocumentByUserName(oferente.UserName) != null);
        }
        public async Task<string> GetProfilePicture(string userName)
        {
            FotosPerfil foto = (await _repositoryFotosPerfil.GetFotosPerfil())
                .SingleOrDefault(f => f.UserName.Equals(userName));
            if (foto == null)
            return string.Empty;

            return $"data:image/{foto.Type};base64,{Convert.ToBase64String(foto.FileContent, 0, foto.FileContent.Length)}";           
        }
        public async Task<string> GetProfilePictureGuid(string userName)
        {
            FotosPerfil foto = (await _repositoryFotosPerfil.GetFotosPerfil())
                .SingleOrDefault(f => f.UserName.Equals(userName));
            if (foto == null)
            return string.Empty;

            return foto.Guid;           
        }
        public async Task<bool> HasProfilePicture(string userName)
        {
            return (await _repositoryFotosPerfil.GetFotosPerfil())
                .SingleOrDefault(f => f.UserName.Equals(userName)) 
                != null;
        }
        #region Helpers
        private async Task<data.Documentos> GetDocumentByUserName(string userName)
        {
            return (await _repositoryDocumentos.GetDocumentos())
                .SingleOrDefault(d => d.UserName.Equals(userName));
        }
        #endregion
    }
}
