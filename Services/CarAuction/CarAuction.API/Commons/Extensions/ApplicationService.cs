using BuildingBlocks.Commons.Behaviors;
using CarAuction.API.Data;
using DomainBlocks.Interceptors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CarAuction.API.Commons.Extensions
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationService).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            // Register FluentValidation
            services.AddValidatorsFromAssembly(typeof(ApplicationService).Assembly);

            services.AddScoped<SaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddDbContext<AuctionDbContext>((sp,opt) =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.AddInterceptors(sp.GetServices<SaveChangesInterceptor>());
            });

            return services;
        }


       
    }
}
