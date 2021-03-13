using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class Provincias
    {
        public Provincias()
        {
            Cantones = new HashSet<Cantones>();
        }

        [Key]
        public int IdProvincia { get; set; }
        [Required]
        [StringLength(150)]
        public string NombreProvincia { get; set; }

        [InverseProperty("IdProvinciaNavigation")]
        public virtual ICollection<Cantones> Cantones { get; set; }
    }
}
