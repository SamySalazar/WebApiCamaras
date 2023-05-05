using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using WebApiCamaras.Filtros;
using WebApiCamaras.Middlewares;
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

            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltroDeExcepcion));
            }).AddJsonOptions(x =>
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

            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddTransient<FiltroDeAccion>();
            services.AddHostedService<EscribirEnArchivo>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPICamaras", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // app.UseMiddleware<ResponseHttpMiddleware>();
            // Metodo para utilizar la clase middleware sin exponer la clase. 
            app.UseResponseHttpMiddleware();

            //app.Use(async (context, siguiente) =>
            //{
            //    //Use me permite agregar mi propio proceso sin afectar a los demas a diferencia de Run
            //    using (var ms = new MemoryStream()) 
            //    {
            //        //Se asigna el body del response en una variable y se le da el valor de memorystream
            //        var originalBody = context.Response.Body;
            //        context.Response.Body = ms;

            //        // Permite a la tubería de procesos continuar
            //        await siguiente.Invoke();

            //        // Guardamos lo que le respondemos al cliente en el string
            //        ms.Seek(0, SeekOrigin.Begin);
            //        string response = new StreamReader(ms).ReadToEnd();
            //        ms.Seek(0, SeekOrigin.Begin);

            //        // Leemos el stream y lo colocamos como estaba
            //        await ms.CopyToAsync(originalBody);
            //        context.Response.Body = originalBody;

            //        logger.LogInformation(response);
            //    }
            //});

            // Para condicionar la ejecucion del middleware segun una ruta especifica se utiliza Map
            // Al utilizar Map permite que en lugar de ejecutar linealmente podemos agregar rutas especificas para
            // nuestro middleware, es como si agregara una rama, o bifurcación en la tubería de procesos
            app.Map("/ruta1", app =>
            {
                // Run permite crear un middleware y cortar la ejecución de los siguientes middlewares
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Interceptando las peticiones");
                });
            });            

            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        } 
    }
}
