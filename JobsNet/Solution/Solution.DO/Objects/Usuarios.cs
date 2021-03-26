using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.DO.Objects
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            //Empresas = new HashSet<Empresas>();
            //Oferentes = new HashSet<Oferentes>();
        }

        public string UserName { get; set; }

        public virtual ICollection<Empresas> Empresas { get; set; }
        //public virtual ICollection<Oferentes> Oferentes { get; set; }
    }
}
