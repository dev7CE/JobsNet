using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.DataModels
{
    public class Documentos
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Guid { get; set; }
        public byte[] FileContent { get; set; }
        public string Type { get; set; }

        public virtual Usuarios Usuario { get; set; }
    }
}
