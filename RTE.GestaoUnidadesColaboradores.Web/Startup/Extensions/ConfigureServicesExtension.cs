using RTE.GestaoUnidadesColaboradores.Application.Applications;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Colaborador;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Unidade;
using RTE.GestaoUnidadesColaboradores.Infra.Interfaces.Usuario;
using RTE.GestaoUnidadesColaboradores.Service.Services;

namespace RTE.GestaoUnidadesColaboradores.Web.Startup.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UsuarioApplication>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ColaboradorApplication>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

            services.AddScoped<UnidadeApplication>();
            services.AddScoped<IUnidadeService, UnidadeService>();
            services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        }
    }
}
