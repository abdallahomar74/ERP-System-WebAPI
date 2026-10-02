using DomainLayer.Contracts;
using ERPSystem.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace ERPSystem.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedingAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true,

                };
                options.DocumentTitle = "ERP System API";
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();
                options.EnablePersistAuthorization();
            });
            return app;
        }
    }
}
