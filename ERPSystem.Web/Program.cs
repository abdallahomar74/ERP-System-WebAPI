using ERPSystem.Web.Extensions;
using Persistence;
using Service;


namespace ERPSystem.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Services
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            #endregion

            var app = builder.Build();


            #region MiddleWares
            await app.SeedDataBaseAsync();
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
