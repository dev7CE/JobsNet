using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class PuestosTrabajo
    {
        public PuestosTrabajo()
        {
            ListaOferentes = new HashSet<ListaOferentes>();
        }

        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal IdPuesto { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal IdEmpresa { get; set; }
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        [StringLength(150)]
        public string Requisitos { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaPublicacion { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaCierre { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.PuestosTrabajo))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [InverseProperty("IdPuestoNavigation")]
        public virtual ICollection<ListaOferentes> ListaOferentes { get; set; }
    }
}
