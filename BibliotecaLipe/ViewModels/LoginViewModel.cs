using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "O campo Usuário é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DisplayName("Lembrar-me")] public bool RemeberMe { get; set; }
}