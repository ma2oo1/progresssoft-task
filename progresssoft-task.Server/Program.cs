using Microsoft.EntityFrameworkCore;
using progresssoft_task.Server.Data;
using progresssoft_task.Server.Repositories.Interfaces;
using progresssoft_task.Server.Repositories;
using progresssoft_task.Server.Services.Interfaces;
using progresssoft_task.Server.Services;
using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);

string appConnectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value!;
// Add services to the container.

//db connection
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(appConnectionString));





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services and repos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBusinessCardService, BusinessCardService>();


// In production, the Angular files will be served from this directory
builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "../../progresssoft-task.client/dist"; });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA from ASP.NET Core, 
    // see https://go.microsoft.com/fwlink/?linkid=864501
    spa.Options.SourcePath = "../../progresssoft-task.client";
    if (app.Environment.IsDevelopment())
    { //spa.Options.StartupTimeout = new TimeSpan(0, 0, 80);
        spa.UseAngularCliServer(npmScript: "start");
    }
});
app.MapFallbackToFile("/index.html");

app.Run();
