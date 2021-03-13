using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.DataModels
{
    public class Provincias
    {
        public int IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
    }
}
