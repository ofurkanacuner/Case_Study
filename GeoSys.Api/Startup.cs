#region - Using

using GeoSys.Services.DBServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

#endregion

namespace GeoSys.Api
{
    public class Startup
    {
        #region - Ctor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region - Variable

        public IConfiguration Configuration { get; }

        #endregion

        #region - ConfigureServices

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddTransient<ICategoriesServices, CategoriesServices>();
            services.AddTransient<IProductsServices, ProductsServices>();

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Case Study",
                    Version = "1.0.0",
                    Description = "Case Study (ASP.NET Core 5.0)",
                    Contact = new OpenApiContact()
                    {
                        Name = "Swagger Implementation Ö. Furkan Acuner",
                        Url = new Uri("https://github.com/ofurkanacuner"),
                        Email = "O.Furkan.Acuner@gmail.com"
                    },
                    TermsOfService = new Uri("http://swagger.io/terms/"),
                });

                c.IncludeXmlComments(Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml"));
            });
        }

        #endregion

        #region - Configure

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Case Study Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
