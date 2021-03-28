using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class ListaOferentes
    {
        public int IdOferente { get; set; }
        public int IdPuesto { get; set; }
        public bool? Descartado { get; set; }

        public virtual Oferentes IdOferenteNavigation { get; set; }
        public virtual PuestosTrabajo IdPuestoNavigation { get; set; }
    }
}
