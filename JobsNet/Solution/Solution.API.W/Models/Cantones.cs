using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class Cantones
    {
        public Cantones()
        {
            Empresas = new HashSet<Empresas>();
        }

        [Key]
        [Column(TypeName = "numeric(10, 0)")]
        public decimal IdCanton { get; set; }
        [Required]
        [StringLength(150)]
        public string NombreCanton { get; set; }
        [Required]
        [StringLength(50)]
        public string IdProvincia { get; set; }

        [ForeignKey(nameof(IdProvincia))]
        [InverseProperty(nameof(Provincias.Cantones))]
        public virtual Provincias IdProvinciaNavigation { get; set; }
        [InverseProperty("IdCantonNavigation")]
        public virtual ICollection<Empresas> Empresas { get; set; }
    }
}
