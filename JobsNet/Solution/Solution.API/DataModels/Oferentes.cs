using System;

namespace Solution.API.DataModels
{
    public partial class Oferentes
    {
        public int IdOferente { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Telefono { get; set; }
        public string UrlCurriculo { get; set; }
        public string UrlFoto { get; set; }
        public string UserName { get; set; }

        public virtual Usuarios Usuario { get; set; }
    }
}
