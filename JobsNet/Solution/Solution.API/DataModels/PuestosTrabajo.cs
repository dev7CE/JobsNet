using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.DataModels
{
    public partial class PuestosTrabajo
    {
        public int IdPuesto { get; set; }
        public int IdEmpresa { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Requisitos { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public virtual Empresas Empresa { get; set; }
    }
}
