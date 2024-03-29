using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SportsStore
{
    #region Program Class

    /// <summary>
    /// Es la clase que se encarga del bootstrapping antes de pasarle el control a `Startup`
    /// </summary>
    /// <remarks>
    /// Para habilitar EFC necesitamos deshabilitar el `ambito de verificación`
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseDefaultServiceProvider(
                        options => options.ValidateScopes = false);
                });
    }

    #endregion Program Class
}