using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class Empresas
    {
        public Empresas()
        {
            PuestosTrabajo = new HashSet<PuestosTrabajo>();
        }

        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public int? IdCanton { get; set; }
        public string UserName { get; set; }

        public virtual Cantones IdCantonNavigation { get; set; }
        public virtual Usuarios UserNameNavigation { get; set; }
        public virtual ICollection<PuestosTrabajo> PuestosTrabajo { get; set; }
    }
}
