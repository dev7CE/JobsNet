using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.DO.Objects
{
    public partial class Provincias
    {
        public int IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
    }
}
