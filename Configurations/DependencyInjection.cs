using SelfLearning.Services.Implementations;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(Mappings.AutoMapperProfile));

        // HttpClient for PNR API
        services.AddHttpClient("PnrApi", client =>
        {
            // Configure base URL if needed
            // client.BaseAddress = new Uri(configuration["PnrApi:BaseUrl"] ?? "https://api.example.com");
        });

        // Services
        services.AddScoped<IPnrApiService, PnrApiService>();
        services.AddScoped<IPassengerService, PassengerService>();

        return services;
    }
}

