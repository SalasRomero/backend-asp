using backend_asp.Controllers;
using backend_asp.Filtros;
using backend_asp.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
            });

            //Vamos a configurar el sistema de autorización
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            //services.AddResponseCaching(); //Agregamso la configuracion del caching

            //Aqui vamos a agregar la configuración de la inyección de dependencias
            //services.AddTransient<RepositorioEnMemoria>();
            //services.AddScoped<IRepositorio,RepositorioEnMemoria>();
            //services.AddScoped<WeatherForecastController>();
            //services.AddTransient<MiFiltroDeAccion>();

            services.AddCors((options)=> {

                var frontend_url = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy((builder)=> {
                    //No hay que colocar el / al  final 
                    builder.WithOrigins(frontend_url).AllowAnyMethod().AllowAnyHeader();
                });
            
            });

            services.AddControllers(options=> {
                options.Filters.Add(typeof(FiltroDeExcepcion));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "backend_asp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Este método es nuestra tuberia de procesos
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //vamos a crear nuestro middleware


            //app.Use(async (context,next)=> {
            //    //Se usa el memorystring porque el cuerpo de la peticion es un string 
            //    using (var swapStream = new MemoryStream()) {
                    
            //        var respuestaOriginal = context.Response.Body;
            //        context.Response.Body = swapStream;

            //        await next.Invoke();

            //        swapStream.Seek(0,SeekOrigin.Begin);
            //        string respuesta = new StreamReader(swapStream).ReadToEnd();
            //        swapStream.Seek(0,SeekOrigin.Begin);

            //        await swapStream.CopyToAsync(respuestaOriginal);
            //        context.Response.Body = respuestaOriginal;

            //        logger.LogInformation(respuesta);

            //    }
            
            //});

            
            //solo se ejecuta si entra en esta ruta
            //Si no entra a esta ruta va ha seguri con el proceso siguiente
            //app.Map("/mapa1",(app)=> {
            //    //Vamos a crear una subtuberia
            //    app.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync("Estoy interceptando la respuesta con un pipeline.");

            //    });
            //});

            //app.Run(async context =>
            //{
                //Recuera que el contexto es la petición que nos esta llegando del cliente
                //Para usar el writeasync hay que importar el siguiente paquete using Microsoft.AspNetCore.Http;
                //await context.Response.WriteAsync("Estoy interceptando la pipeline.");
            //});
            //Solo se ejecuta cuando es de desarollo
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "backend_asp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            //app.UseResponseCaching();//Middleware que activa el response caching

            app.UseAuthentication();

            //Este middleware simpre va entre UseRouting y UseEnpoints
            app.UseAuthorization(); // Este middleware nos permite tener la capacidad de tener autorizaciones

            app.UseEndpoints(endpoints =>
            {
                //Este middleware es el que configura nuestros controladores
                endpoints.MapControllers();
            });
        }
    }
}
