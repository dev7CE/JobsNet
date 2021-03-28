using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class Provincias
    {
        public Provincias()
        {
            Cantones = new HashSet<Cantones>();
        }

        public int IdProvincia { get; set; }
        public string NombreProvincia { get; set; }

        public virtual ICollection<Cantones> Cantones { get; set; }
    }
}
