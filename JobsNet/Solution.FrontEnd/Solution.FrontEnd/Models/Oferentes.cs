using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.FrontEnd.Models
{
    public partial class Oferentes
    {
        public Oferentes()
        {
            //ListaOferentes = new HashSet<ListaOferentes>();
        }

        public int IdOferente { get; set; }
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        [StringLength(150)]
        public string Apellido1 { get; set; }
        [StringLength(150)]
        public string Apellido2 { get; set; }
        [StringLength(150)]
        public string Telefono { get; set; }
        [StringLength(260)]
        public string UrlCurriculo { get; set; }
        [StringLength(260)]
        public string UrlFoto { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public virtual Usuarios Usuario { get; set; }
        //public virtual ICollection<ListaOferentes> ListaOferentes { get; set; }
    }
}
