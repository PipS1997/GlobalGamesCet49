namespace GlobalGamesCet49{
    using GlobalGamesCet49.Dados;
    using Microsoft.AspNetCore;    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }
        private static void RunSeeding(IWebHost host)
        {
            var scopeXatory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeXatory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDB>();
                seeder.SeedAsync().Wait();

            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
