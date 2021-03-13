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
        public int IdCanton { get; set; }
        [Required]
        [StringLength(150)]
        public string NombreCanton { get; set; }
        public int IdProvincia { get; set; }

        [ForeignKey(nameof(IdProvincia))]
        [InverseProperty(nameof(Provincias.Cantones))]
        public virtual Provincias IdProvinciaNavigation { get; set; }
        [InverseProperty("IdCantonNavigation")]
        public virtual ICollection<Empresas> Empresas { get; set; }
    }
}
