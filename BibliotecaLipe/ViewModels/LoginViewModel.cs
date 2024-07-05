using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Nome de usuário obrigatório")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DisplayName("Lembrar-me")] public bool RemeberMe { get; set; }
}