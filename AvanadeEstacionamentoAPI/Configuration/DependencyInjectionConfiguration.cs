using AvanadeEstacionamento.Data.Context;
using AvanadeEstacionamento.Data.Repository;
using AvanadeEstacionamento.Domain.AutoMapper;
using AvanadeEstacionamento.Domain.Interfaces.Repository;
using AvanadeEstacionamento.Domain.Interfaces.Service;
using AvanadeEstacionamento.Domain.Services;

namespace AvanadeEstacionamento.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Database Injection

            services.AddScoped<MyDbContext>();

            #endregion

            #region Veiculo Injection

            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IVeiculoService, VeiculoService>();

            #endregion

            #region Estacionamento Injection

            services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();
            services.AddScoped<IEstacionamentoService, EstacionamentoService>();

            #endregion

            #region AutoMapper Injection

            services.AddAutoMapper(typeof(AutoMapperProfile));

            #endregion

            return services;
        }
    }
}
