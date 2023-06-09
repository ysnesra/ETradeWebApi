using Business.DependencyResolvers;
using Business.Utilities.Security.Encryption;
using Business.Utilities.Security.JWT;
using DataAccess.Concrete.Entityframework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthorization();

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

//JWTBearer kullan�laca�� belirtilir 
// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(X => X.Cookie.Name = "token")
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                context.Response.Redirect("/Home/AccessDenied");
                return Task.CompletedTask;
            }
        };       
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//***Http yap�s�n� b�t�n katmanalarda kullanmam�z� sa�lar*******
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
