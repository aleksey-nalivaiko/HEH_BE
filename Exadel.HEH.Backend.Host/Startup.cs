using Exadel.HEH.Backend.BusinessLogic;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using OData.Swagger.Services;

namespace Exadel.HEH.Backend.Host
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
            services.AddControllers();
            services.AddRepositories(Configuration);
            services.AddCrudServices();

            services.AddOData();
            services.AddSwaggerGen();
            services.AddOdataSwaggerSupport();

            services.AddSingleton(MapperExtensions.Mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Filter();
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<DiscountDto>("Discounts");

            return odataBuilder.GetEdmModel();
        }
    }
}
