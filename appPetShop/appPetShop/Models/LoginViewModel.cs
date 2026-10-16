using System.ComponentModel.DataAnnotations;

namespace appPetShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF do Cliente")]
        public string Cpf { get; set; } = string.Empty;
    }
}
