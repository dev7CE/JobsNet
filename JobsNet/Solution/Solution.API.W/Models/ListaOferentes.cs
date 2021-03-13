using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class ListaOferentes
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal IdOferente { get; set; }
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal IdPuesto { get; set; }
        public bool? Descartado { get; set; }

        [ForeignKey(nameof(IdOferente))]
        [InverseProperty(nameof(Oferentes.ListaOferentes))]
        public virtual Oferentes IdOferenteNavigation { get; set; }
        [ForeignKey(nameof(IdPuesto))]
        [InverseProperty(nameof(PuestosTrabajo.ListaOferentes))]
        public virtual PuestosTrabajo IdPuestoNavigation { get; set; }
    }
}
