using System.Threading.Tasks;
using Git.Data;
using Git.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

namespace Git
{
    public class StartUp
    {
        public static async Task Main()
        => await HttpServer
            .WithRoutes(routes => routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<GitDbContext>()
                .Add<IValidator, Validator>()
                .Add<IPasswordHasher, PasswordHasher>())
            .WithConfiguration<GitDbContext>(context => context
                    .Database.Migrate())
            .Start();
    }
}
