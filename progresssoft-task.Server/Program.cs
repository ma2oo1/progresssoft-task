using Microsoft.EntityFrameworkCore;
using progresssoft_task.Server.Data;
using progresssoft_task.Server.Repositories.Interfaces;
using progresssoft_task.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

string appConnectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value!;
// Add services to the container.

//db connection
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(appConnectionString));


//services and repos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
