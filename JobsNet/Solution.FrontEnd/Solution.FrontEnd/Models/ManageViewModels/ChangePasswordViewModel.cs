using System.ComponentModel.DataAnnotations;

namespace Solution.FrontEnd.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required (ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required (ErrorMessage = "Este campo es requerido")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de extensión.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña nueva")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
