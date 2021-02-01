using System.Text.Json.Serialization;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using Exadel.HEH.Backend.Host.Extensions;
using Exadel.HEH.Backend.Host.Identity;
using Exadel.HEH.Backend.Host.SwaggerFilters;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(validation =>
                {
                    validation.RegisterValidatorsFromAssemblyContaining<FavoritesValidator>();
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddOData().EnableApiVersioning();
            services.AddODataApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Happy Exadel Hours API {groupName}",
                    Version = groupName
                });

                options.OperationFilter<DefaultValuesOperationFilter>();
                options.OperationFilter<HideApiVersionOperationFilter>();
                options.OperationFilter<CountParameterOperationFilter>();
            });

            services.AddOdataSwaggerSupport();

            services.AddUserProvider();
            services.AddRepositories(Configuration);
            services.AddCrudServices();
            services.AddValidators();
            services.AddValidationServices();
            services.AddSingleton(MapperExtensions.Mapper);

            services.AddIdentityServer()
                .AddClients()
                .AddIdentityApiResources()
                .AddPersistedGrants()
                .AddUsers()
                .AddDeveloperSigningCredential();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Authority"];
                    options.ApiName = "exadel_heh_api";
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseODataRouting();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Filter().Count().OrderBy().Select().Expand();
                endpoints.MapVersionedODataRoute("odata", "odata", modelBuilder.GetEdmModels());
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Happy Exadel Hours API V1"));

            SeedIdentityData.InitializeDatabaseAsync(app).Wait();
        }
    }
}