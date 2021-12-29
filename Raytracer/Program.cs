using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raytracer;
using Raytracer.Extensions;

var configuration = new ConfigurationBuilder()
    .BuildConfigurationRoot();

var services = new ServiceCollection()
    .ConfigureServices(configuration)
    .BuildServiceProvider();

var raytracer = services.GetRequiredService<App>();
await raytracer.Run();
