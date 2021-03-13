using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class Empresas
    {
        public Empresas()
        {
            PuestosTrabajo = new HashSet<PuestosTrabajo>();
        }

        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal IdEmpresa { get; set; }
        [Required]
        [StringLength(150)]
        public string NombreEmpresa { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        [StringLength(150)]
        public string Telefono { get; set; }
        [Column(TypeName = "numeric(10, 0)")]
        public decimal? IdCanton { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [ForeignKey(nameof(IdCanton))]
        [InverseProperty(nameof(Cantones.Empresas))]
        public virtual Cantones IdCantonNavigation { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Usuarios.Empresas))]
        public virtual Usuarios UserNameNavigation { get; set; }
        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<PuestosTrabajo> PuestosTrabajo { get; set; }
    }
}
