using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class PuestosTrabajo
    {
        public PuestosTrabajo()
        {
            ListaOferentes = new HashSet<ListaOferentes>();
        }

        public int IdPuesto { get; set; }
        public int IdEmpresa { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime FechaCierre { get; set; }

        public virtual Empresas IdEmpresaNavigation { get; set; }
        public virtual ICollection<ListaOferentes> ListaOferentes { get; set; }
    }
}
