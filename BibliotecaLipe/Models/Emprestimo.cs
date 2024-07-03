using System.ComponentModel.DataAnnotations;
using Biblioteca.Models;
using Biblioteca.Models.Enums;
using Microsoft.AspNetCore.Identity;

public class Emprestimo
{
    public int EmprestimoId { get; set; }
    public DateTime DataEmprestimo { get; set; } = DateTime.Now.Date;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Devolução")]
    [CustomValidation(typeof(Emprestimo), nameof(ValidarDataDevolucao))]
    public DateTime DataDevolucao { get; set; }

    public Status Status { get; set; }
    public string UsuarioId { get; set; } 
    public IdentityUser Usuario { get; set; } 
    public int LivroId { get; set; } 
    public Livro Livro { get; set; } 

    public static ValidationResult ValidarDataDevolucao(DateTime dataDevolucao, ValidationContext context)
    {
        if (dataDevolucao < DateTime.Today)
        {
            return new ValidationResult("A data de devolução não pode ser anterior à data atual.");
        }
        return ValidationResult.Success;
    }
}