using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Empresas = new HashSet<Empresas>();
            Oferentes = new HashSet<Oferentes>();
        }

        [Key]
        [StringLength(256)]
        public string UserName { get; set; }

        [InverseProperty("UserNameNavigation")]
        public virtual ICollection<Empresas> Empresas { get; set; }
        [InverseProperty("UserNameNavigation")]
        public virtual ICollection<Oferentes> Oferentes { get; set; }
    }
}
