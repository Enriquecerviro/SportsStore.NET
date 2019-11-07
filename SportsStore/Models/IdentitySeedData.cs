using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    /// <summary>
    /// Esta clase crea explicitamente el admin by seeding the DB cuando la app comienza.
    /// </summary>
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        /// <summary>
        /// Ensures the populated. Usamos la clase UserManager-T- que está proveído como
        /// servicio por ASP.NET Core Identity para controlar usuarios. De esta manera la DB
        /// buscará la cuenta de usuario "Admin" que si no está pues la creará al inicio,
        /// para ello se añade el statement(EnsurePopulated) en el método configure de StartUp
        /// </summary>
        /// <param name="app">The application.</param>
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}