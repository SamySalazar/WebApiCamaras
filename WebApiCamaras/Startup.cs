using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using WebApiCamaras.Services;

namespace WebApiCamaras
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        public IConfiguration Configuration { get; }
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            // Servicio de tipo transitorio
            // Cuando se solicite resolver el IService, se va a dar una nueva instancia de la clase ServicioA
            services.AddTransient<IService, ServiceA>(); // Para funcnion simples, sin tener que mantener datos que van a ser reutilizados en otros lugares  
            // services.AddTransient<ServiceA>();
            services.AddTransient<ServiceTransient>();

            // Servicio de tipo Scoped
            // El tiepo de vida de la clase ServiceA aumenta,
            // ahora en vez de ser siempre una instancia distinta,
            // ahora va a ser, dentro del mismo contexto http se nos va a dar exactamentne la misma instancia, sin embargoo,
            // entre distintas peticiones http, habrá distintas instancias   
            //services.AddScoped<IService, ServiceA>(); // Por ejemplo ApplicationDbContext, que usa la misma instancia.
            services.AddScoped<ServiceScoped>();

            // Servicio Singleton
            // Se tiene siempre la misma instancia,
            // incluso para distintos usuarios con distintas peticiones http.
            // services.AddSingleton<IService, ServiceA>(); // Sirve si se tiene algún tipo de datos en cache, para encontrarla rápido a todos los usuarios.
            services.AddSingleton<ServiceSingleton>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPICamaras", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        } 
    }
}
