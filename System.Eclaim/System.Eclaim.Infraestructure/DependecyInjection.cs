using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Eclaim.Domain.OutPort.Persistence;
using System.Eclaim.Domain.OutPort.Secrets;
using System.Eclaim.Domain.OutPort.Services;
using System.Eclaim.Infraestructure.Adapters.Persistence;
using System.Eclaim.Infraestructure.Adapters.Services;
using System.Eclaim.Infraestructure.Configurations.Context;
using System.Eclaim.Infraestructure.Configurations.Secrets;
using System.Eclaim.Shared.Constants;

namespace System.Eclaim.Infraestructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Integrar Vault
            services.AddVaultSecrets(configuration);

            var sp = services.BuildServiceProvider();
            var secretProvider = sp.GetRequiredService<IVaultSecretsProvider>();
            var secrets = secretProvider.GetSecretsAsync().GetAwaiter().GetResult(); // sincronizar

            // Inyección del contexto de base de datos
            services.AddDbContext<IdentityDbContext>((sp, options) =>
            {
                var cs = secrets["DbSecurity"];
                options.UseNpgsql(cs);
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisCon = secrets["RedisConnetion"];
                return ConnectionMultiplexer.Connect(redisCon);
            });

            services.AddHttpClient<CaptchaService>();
            services.AddScoped<IReclamoRepository, ReclamoRepository>();
            services.AddScoped<ICaptchaService, CaptchaService>();
            return services;
        }
    }
}
