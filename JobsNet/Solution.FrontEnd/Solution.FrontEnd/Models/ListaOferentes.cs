using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.FrontEnd.Models
{
    public partial class ListaOferentes
    {
        [Key]
        public int IdOferente { get; set; }
        [Key]
        public int IdPuesto { get; set; }
        public bool? Descartado { get; set; }

        public virtual Oferentes Oferente { get; set; }
        public virtual PuestosTrabajo PuestoTrabajo { get; set; }
    }
}
