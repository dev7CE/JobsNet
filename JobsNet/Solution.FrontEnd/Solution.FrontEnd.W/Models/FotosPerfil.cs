using System;
using System.Collections.Generic;

namespace Solution.FrontEnd.W.Models
{
    public partial class FotosPerfil
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Guid { get; set; }
        public byte[] FileContent { get; set; }
        public string Type { get; set; }

        public virtual Usuarios UserNameNavigation { get; set; }
    }
}
