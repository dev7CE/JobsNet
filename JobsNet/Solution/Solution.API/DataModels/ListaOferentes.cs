using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.DataModels
{
    public partial class ListaOferentes
    {
        public int IdOferente { get; set; }
        public int IdPuesto { get; set; }
        public bool? Descartado { get; set; }

        public virtual Oferentes Oferente { get; set; }
        public virtual PuestosTrabajo PuestoTrabajo { get; set; }
    }
}
