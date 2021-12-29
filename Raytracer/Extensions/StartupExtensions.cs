using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raytracer.Services;

namespace Raytracer.Extensions
{
    internal static class StartupExtensions
    {
        public static IConfigurationRoot BuildConfigurationRoot(this IConfigurationBuilder configurationBuilder)
        {
            var configuration = configurationBuilder
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            return configuration;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configurationRoot)
        {
            services
                .AddSingleton(configurationRoot)
                .AddSingleton<App>()
                .Configure<AppSettings>(configurationRoot)
                .AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IFileService, P3FileService>()
                .AddScoped<IImageService, P3ImageService>();

            return services;
        }
    }
}
