using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using Exam.Data;
using Exam.Services;
using Microsoft.EntityFrameworkCore;

namespace Exam
{
    public class StartUp
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<ExamDbContext>()
                    .Add<IValidator, Services.Validator>()
                    .Add<IPasswordHasher, PasswordHasher>()
                    .Add<IViewEngine, CompilationViewEngine>())
                .WithConfiguration<ExamDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
