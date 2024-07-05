    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    namespace Biblioteca.Models;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")] [EmailAddress] public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Usuario é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; } 

        [DataType(DataType.Password)]
        [DisplayName("Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais")]
        public string? ConfirmaPassword { get; set; }
    }