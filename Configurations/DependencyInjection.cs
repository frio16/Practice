using Microsoft.EntityFrameworkCore;
using SelfLearning.Data;
using SelfLearning.Repositories.Implementations;
using SelfLearning.Repositories.Interfaces;
using SelfLearning.Services.Implementations;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("SelfLearningDb"));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddScoped<IUserService, UserService>();

        // AutoMapper
        services.AddAutoMapper(typeof(Mappings.AutoMapperProfile));

        return services;
    }
}

