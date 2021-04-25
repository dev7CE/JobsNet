using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Documentos = new HashSet<Documentos>();
            Empresas = new HashSet<Empresas>();
            FotosPerfil = new HashSet<FotosPerfil>();
            Oferentes = new HashSet<Oferentes>();
        }

        public string UserName { get; set; }

        public virtual ICollection<Documentos> Documentos { get; set; }
        public virtual ICollection<Empresas> Empresas { get; set; }
        public virtual ICollection<FotosPerfil> FotosPerfil { get; set; }
        public virtual ICollection<Oferentes> Oferentes { get; set; }
    }
}
