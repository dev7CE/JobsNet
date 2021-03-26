using System;

namespace Solution.DO.Objects
{
    public class Empresas
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public int? IdCanton { get; set; }
        public string UserName { get; set; }
        public virtual Cantones Canton { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
