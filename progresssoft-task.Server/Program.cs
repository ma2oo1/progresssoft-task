using Microsoft.EntityFrameworkCore;
using progresssoft_task.Server.Data;

var builder = WebApplication.CreateBuilder(args);

string appConnectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value!;
// Add services to the container.
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(appConnectionString));
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
