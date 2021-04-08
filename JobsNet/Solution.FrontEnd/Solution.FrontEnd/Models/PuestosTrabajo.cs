using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.FrontEnd.Models
{
    public partial class PuestosTrabajo
    {
        public PuestosTrabajo()
        {
            ListaOferentes = new HashSet<ListaOferentes>();
        }

        public int IdPuesto { get; set; }
        public int IdEmpresa { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [StringLength(150)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [StringLength(150)]
        public string Requisitos { get; set; }
        [Display(Name = "Fecha de Publicación")]
        public DateTime? FechaPublicacion { get; set; }
        [Display(Name = "Fecha de Cierre")]
        public DateTime FechaCierre { get; set; }

        public virtual Empresas Empresa { get; set; }
        public virtual ICollection<ListaOferentes> ListaOferentes { get; set; }
    }
}
