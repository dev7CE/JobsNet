﻿using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.Models
{
    public partial class Cantones
    {
        public Cantones()
        {
            //Empresas = new HashSet<Empresas>();
        }

        public int IdCanton { get; set; }
        public string NombreCanton { get; set; }
        public int IdProvincia { get; set; }

        public virtual Provincias Provincia { get; set; }
        public virtual ICollection<Empresas> Empresas { get; set; }
    }
}
