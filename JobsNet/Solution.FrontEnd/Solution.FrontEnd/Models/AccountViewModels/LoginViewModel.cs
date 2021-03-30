using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.FrontEnd.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Este campo es requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "¿Recordarme?")]
        public bool RememberMe { get; set; }
    }
}
