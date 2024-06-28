    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    namespace Biblioteca.Models;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Digite o email")] [EmailAddress] public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o nome de usuário")]
        public string? Nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } 

        [DataType(DataType.Password)]
        [DisplayName("Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais")]
        public string? ConfirmaPassword { get; set; }
    }