using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Exadel.HEH.Backend.BusinessLogic;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.Host.Controllers;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;

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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = false;
            });
            services.AddOData().EnableApiVersioning();
            services.AddODataApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = false;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    //options.QueryOptions.Controller<DiscountController>()
                    //            .Action(c => c.Get())
                    //            .AllowFilter(default)
                    //            .Allow(Skip | Count | Filter)
                    //            .AllowTop(100);
                });

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Happy Exadel Hours API {groupName}",
                    Version = groupName
                });
                options.OperationFilter<SwaggerDefaultValues>();

                options.OperationFilter<HideApiVersionDocumentFilter>();

                options.OperationFilter<CountParameterOperationFilter>();
            });

            //var conv = new ODataSwaggerConverter(GetEdmModel());
            //var json = conv.GetSwaggerModel();
            //var doc = json.ToObject<OpenApiDocument>();

            services.AddOdataSwaggerSupport();

            services.AddRepositories(Configuration);
            services.AddCrudServices();
            services.AddSingleton(MapperExtensions.Mapper);
        }

        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IWebHostEnvironment env)
           /* IApiVersionDescriptionProvider provider*/
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Filter();
                endpoints.MapVersionedODataRoute("odata", "odata", modelBuilder.GetEdmModels());
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Happy Exadel Hours API V1");
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                //        description.GroupName.ToUpperInvariant());
                //}

                //options.RoutePrefix = string.Empty;
            });
        }
    }
}
