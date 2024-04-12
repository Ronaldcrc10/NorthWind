using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Entities.Interfaces;
using NorthWind.Presenters;
using NorthWind.Repositories.EFCore.DataContext;
using NorthWind.Repositories.EFCore.Repositories;
using NorthWind.Use.CasesPorts.CreateOrder;
using NorthWind.UseCases.Common.Validators;
using NorthWind.UseCases.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddNorthWindServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NorthWindContext>(options => options.UseSqlServer(configuration.GetConnectionString("NorthWindDB")));
            services.AddScoped<IOrderRepository, IOrderRepository>();
            services.AddScoped<IOrderDetailRepository, IOrderDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddValidatorsFromAssembly(typeof(CreateOrderValidator).Assembly);
            services.AddScoped<ICreateOrderInputPort, CreateOrderInteractor>();
            services.AddScoped<ICreateOrderOutputPort, CreateOrderPresenter>();
                return services;
        }
    }
}
