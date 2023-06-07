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
            var connectingString = configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<ETradeAppDbContext>(opt =>
            {
                opt.UseMySql(connectingString, ServerVersion.AutoDetect(connectingString));
            });

            return services;
        }
    }
}
