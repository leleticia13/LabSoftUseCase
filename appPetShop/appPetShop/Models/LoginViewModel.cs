using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF para a verificação")]
        public string Cpf { get; set; } = string.Empty;
    }
}
