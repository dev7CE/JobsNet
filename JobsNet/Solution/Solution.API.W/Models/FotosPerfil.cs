using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.API.W.Models
{
    public partial class FotosPerfil
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        public string Guid { get; set; }
        [Required]
        public byte[] FileContent { get; set; }
        [Required]
        [StringLength(256)]
        public string Type { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Usuarios.FotosPerfil))]
        public virtual Usuarios UserNameNavigation { get; set; }
    }
}
