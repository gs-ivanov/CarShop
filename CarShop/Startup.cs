namespace CarShop
{
    using System.Threading.Tasks;
    using CarShop.Data;
    using CarShop.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;

    class Startup
    {
        public static async Task Main()
         => await HttpServer
          .WithRoutes(routes => routes
              .MapStaticFiles()
              .MapControllers())
          .WithServices(services => services
              .Add<IUserService, UserService>()
              .Add<IValidator, Validator>()
              .Add<IPasswordHasher, PasswordHasher>()
              .Add<IViewEngine, CompilationViewEngine>()
              .Add<CarShopDbContext>())
          .WithConfiguration<CarShopDbContext>(context=> context
          .Database.Migrate())
          .Start();
    }
}
