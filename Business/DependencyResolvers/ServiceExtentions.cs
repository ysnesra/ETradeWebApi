using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public static class ServiceExtentions
    {
        public static IServiceCollection ServisRelationShip(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductDal>();

            var connectingString = configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<ETradeAppDbContext>(opt =>
            {
                opt.UseMySql(connectingString, ServerVersion.AutoDetect(connectingString));
            });

            return services;
        }
    }
}
