using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RTE.GestaoUnidadesColaboradores.Infra;
using RTE.GestaoUnidadesColaboradores.Web.Startup.Extensions;
using RTE.GestaoUnidadesColaboradores.Web.Startup.Middlewares;
using System.Reflection;

namespace RTE.GestaoUnidadesColaboradores.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<BusinessExceptionFilter>();
            });

            services.AddDbContext<RTEGestaoUnidadesColaboradoresDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("RTEConnection")));

            services.AddEndpointsApiExplorer();

            var swaggerXMLPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RTEGestaoUidadesColaboradores API",
                    Description = "Projeto de teste para avaliação da RTE"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization Header.<br />
                                Preencha: 'Bearer' [espaço] seu token.<br />
                                Exemplo: 'Bearer 12345abcdef...'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme, Id = "Bearer"
                      }
                    },
                    Array.Empty<string>()
                  }
                });

                c.IncludeXmlComments(swaggerXMLPath);
                c.EnableAnnotations();
            });

            services.AddServices();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddJwtAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RTEGestaoUidadesColaboradoresAPIv1");
            });

            app.UseCors("AllowAll");
        }
    }
  }
