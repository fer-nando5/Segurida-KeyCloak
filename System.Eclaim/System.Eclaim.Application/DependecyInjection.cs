using Microsoft.Extensions.DependencyInjection;
using System.Eclaim.Application.InPorts.Captcha;
using System.Eclaim.Application.InPorts.Reclamos;
using System.Eclaim.Application.UseCases.Captcha;
using System.Eclaim.Application.UseCases.Reclamos;

namespace System.Eclaim.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICreateReclamoUseCase, CreateReclamoUseCase>();
            services.AddScoped<IFindByReclamoUseCase, FindByReclamoUseCase>();
            services.AddScoped<IVerifyCaptchaUseCase, VerifyCaptchaUseCase>();
            services.AddScoped<IRedeemUseCase, RedeemUseCase>();
            services.AddScoped<ICreateChallengeUseCase, CreateChallengeUseCase>();
            return services;
        }
    }
}
