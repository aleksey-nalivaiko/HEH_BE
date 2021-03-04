using System.Diagnostics.CodeAnalysis;
using Exadel.HEH.Backend.Host.Identity.Store;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Identity
{
    [ExcludeFromCodeCoverage]
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddClients(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, CustomClientStore>();
            builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();

            return builder;
        }

        public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IResourceStore, CustomResourceStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IPersistedGrantStore, CustomPersistedGrantStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddUsers(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<UserProfileService>();
            builder.AddResourceOwnerValidator<UserResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}