using Business.DependencyResolvers;
using DataAccess.Concrete.Entityframework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// IocContainer
builder.Services.AddBusinessService();   //.net in kendi Ioc Container�n� kulllan�r�z.//DependencyEnjection.cs 'de olu�turugum metotu ekliyorum
builder.Services.ServisRelationShip(builder.Configuration);  //.net in kendi IoC Container�nda katmanlar�n� ilil�ksini yazd���m extention metotu ekliyorum //ServisRelationShip() -> ServiceExtention.cs de olu�turdu�um metot
#region MyServices
//var connectingString = builder.Configuration.GetConnectionString("MySqlConnection");
//builder.Services.AddDbContext<ETradeAppDbContext>(opt =>
//{
//    opt.UseMySql(connectingString, ServerVersion.AutoDetect(connectingString));
//});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
