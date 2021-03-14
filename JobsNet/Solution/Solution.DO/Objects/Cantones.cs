using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.DO.Objects
{
    public partial class Cantones
    {
        public int IdCanton { get; set; }
        public string NombreCanton { get; set; }
        public int IdProvincia { get; set; }
        public virtual Provincias Provincia { get; set; }
    }
}
