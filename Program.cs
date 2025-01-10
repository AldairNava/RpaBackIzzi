using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using WebApplication1;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.Hosting;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Sustainsys.Saml2;
using Sustainsys.Saml2.AspNetCore2;
using Sustainsys.Saml2.Metadata;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración de SAML2
builder.Services.AddAuthentication(opt =>
{
    // Default scheme that maintains session is cookies.
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    // If there's a challenge to sign in, use the Saml2 scheme.
    opt.DefaultChallengeScheme = Saml2Defaults.Scheme;
})
.AddCookie("cookie", opt =>
{
    // Cuando se desafíe, redirigir a esta ruta. Esto captura
    // cualquier página intentada como un parámetro de consulta returnUrl.
    opt.LoginPath = "/Login";
})
.AddCookie("external")
.AddSaml2(Saml2Defaults.Scheme, opt =>
{
    // When Saml2 finishes, persist the resulting identity
    // in the external cookie.
    opt.SignInScheme = "external";

    // Set up our EntityId, this is our application.
    opt.SPOptions.EntityId = new EntityId("https://rpabackizzi.azurewebsites.net");

    // Add an identity provider.
    opt.IdentityProviders.Add(new IdentityProvider(
        // The identity provider's entity id.
        new EntityId("https://sts.windows.net/3ad84793-7b5f-4519-84ce-96790471f26a/"),
        opt.SPOptions)
    {
        // Load configuration parameters from metadata
        LoadMetadata = true,
        MetadataLocation = "https://compartidacyber.blob.core.windows.net/mariana/portalIZZI.xml",

        // No need to add ServiceCertificates if only validating incoming messages
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "MyAllowSpecificOrigins", builder => {
        builder.WithOrigins("http://localhost:4200", "https://frontrpaizzi.azurewebsites.net", "http://192.168.49.76").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<IScheduler>(provider => provider.GetRequiredService<ISchedulerFactory>().GetScheduler().Result);

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});
builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});

var wsPptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(10) };
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("MyAllowSpecificOrigins");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets(wsPptions);

app.MapControllers();
app.MapRazorPages();

app.MapDefaultControllerRoute();  // Asegúrate de que esto esté presente para el enrutamiento por defecto

app.Run();
