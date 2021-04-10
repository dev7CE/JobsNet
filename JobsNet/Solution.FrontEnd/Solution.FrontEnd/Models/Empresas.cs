using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.FrontEnd.Models
{
    public partial class Empresas
    {
        public Empresas()
        {
            //PuestosTrabajo = new HashSet<PuestosTrabajo>();
        }

        public int IdEmpresa { get; set; }
        [Required]
        [StringLength(150)]
        public string NombreEmpresa { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        [StringLength(150)]
        public string Telefono { get; set; }
        public int? IdCanton { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public virtual Cantones Canton { get; set; }
        public virtual Usuarios Usuario { get; set; }
        //public virtual ICollection<PuestosTrabajo> PuestosTrabajo { get; set; }
    }
}
