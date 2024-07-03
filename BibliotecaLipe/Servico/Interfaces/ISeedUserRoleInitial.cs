using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Servico.Interfaces;

public interface ISeedUserRoleInitial
{
    Task SeedRolesAsync();
    Task SeedUsersAsync();
}