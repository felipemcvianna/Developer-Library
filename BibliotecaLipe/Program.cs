using Biblioteca.Controllers;
using Biblioteca.Data;
using Biblioteca.Servico;
using Biblioteca.Servico.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 37))));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BibliotecaDbContext>();

builder.Services.AddScoped<ServicoLivros>();
builder.Services.AddScoped<ServicoEmprestimo>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ServicoListaDesejo>();
builder.Services.AddScoped<Logger<ServicoEmprestimo>>();
builder.Services.AddScoped<Logger<ServicoLivros>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
await CriarPerfisUsuariosAsync(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livro}/{action=Index}/{id?}");

app.Run();

async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var ScopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = ScopeFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        await service.SeedRolesAsync();
        await service.SeedUsersAsync();
    }
}