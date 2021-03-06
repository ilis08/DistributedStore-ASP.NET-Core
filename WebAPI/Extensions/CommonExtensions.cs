using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Implementations;
using Repository.Implementations.ProductRepo;
using WebAPI.Filters;
using WebAPI.Messages;

namespace WebAPI.Extensions
{
    public static class CommonExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IlisStoreAPI", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebAPI.xml");
                c.IncludeXmlComments(filePath);
                c.EnableAnnotations();
            });
        }

        public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
              opts.UseSqlServer(configuration.GetConnectionString("IlisStore"),
              b => b.MigrationsAssembly("WebAPI")));
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<UnitOfWork>();
        }

        public static void ConfigureResponseMessage(this IServiceCollection services)
        {
            services.AddTransient<ResponseMessage>();
        }

        public static void ConfigureProductImageService(this IServiceCollection services)
        {
            services.AddTransient<ProductImage>();
        }

        public static void ConfigureValidationFilterAttribute(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("https://www.ilisstoreclient.somee.com/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders(
                (expiration) =>
                {
                    expiration.MaxAge = 180;
                    expiration.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
                );
    }
}
