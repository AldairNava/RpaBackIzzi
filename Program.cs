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

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = Saml2Defaults.Scheme;
})
.AddCookie("cookie", opt =>
{
    opt.LoginPath = "/Login";
})
.AddCookie("external")
.AddSaml2(Saml2Defaults.Scheme, opt =>
{
    opt.SignInScheme = "external";
    opt.SPOptions.EntityId = new EntityId("https://rpabackizzi.azurewebsites.net/Saml2");
    opt.IdentityProviders.Add(new IdentityProvider(
        new EntityId("https://sts.windows.net/caca42e2-ad4a-4d19-824c-3ff3709bd840/"),
        opt.SPOptions)
    {
        LoadMetadata = true,
        MetadataLocation = "https://compartidacyber.blob.core.windows.net/mariana/AzureADSSOMariana.xml",
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "MyAllowSpecificOrigins", builder => {
        builder.WithOrigins("http://localhost:4200", "https://frontrpaizzi.azurewebsites.net", "http://192.168.49.76", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("MyAllowSpecificOrigins");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets(wsPptions);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapFallbackToFile("/index.html");
});

app.Run();

