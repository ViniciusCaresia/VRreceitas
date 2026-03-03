using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VRreceitas.Data;
using VRreceitas.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Serviço de conexão do contexto
string conexao = builder.Configuration.GetConnectionString("conexao");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer (conexao)
);

// Configuração do serviço do Identity
builder.Services.AddIdentity<Usuario, IdentityRole>(
    Options =>
    {
        Options.SignIn.RequireConfirmedEmail = false;
        Options.User.RequireUniqueEmail = true;
    }
)
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Código para garantir a existência do banco ao execultar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
