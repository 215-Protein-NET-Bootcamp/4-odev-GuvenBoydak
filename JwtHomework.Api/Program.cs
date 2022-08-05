using FluentValidation.AspNetCore;
using JwtHomework.Api;
using JwtHomework.Base;
using JwtHomework.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Ef core
string db = builder.Configuration.GetConnectionString("PosgreSql");
builder.Services.AddDbContext<EfHomeworkDbContex>(options => options
   .UseNpgsql(db)
   );


//Extension Service Injection
builder.Services.AddDependencyInjection();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Serilog
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
Log.Information("Application is starting.");

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    IdentityModelEventSource.ShowPII = true;
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1); // Remove Schema on Swagger UI
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patika");
    c.DocumentTitle = "Patika";
});

app.UseHttpsRedirection();

//Exceptionları handler etigimiz middleware
app.UseCustomExeption();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
