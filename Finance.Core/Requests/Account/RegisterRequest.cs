using System.ComponentModel.DataAnnotations;

namespace Finance.Core.Requests.Account
{
    public class RegisterRequest  :Request
    {
        [Required(ErrorMessage ="Informe o e-mail")]
        [EmailAddress(ErrorMessage ="Informe um e-mail válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; } = string.Empty;


    }
}
