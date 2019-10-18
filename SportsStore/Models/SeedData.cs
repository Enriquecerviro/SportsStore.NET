using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    #region SeedData Class    
    /// <summary>
    /// Clase que provee a la DB de los datos necesarios
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// Ensures the populated. Este mth. recibe un argumento IApplicationBuilder, que 
        /// es la interfaz usada en el mth `Configure` de la clase `Startup` para registrar los
        /// componentes middleware para manejar las peticiones HTTP, asi que aqui es donde aseguro
        /// que la DB tiene contenido.
        /// </summary>
        /// <remarks>
        /// El Mth. EnsurePopulated obtiene un objeto ApplicationDbContext a través de la interfaz IApplicationBuilder
        /// y llama a la Database.Migrate para asegurar que la migracion ha sido aplicada, lo que significa que la DB
        /// se creará y se preparará así se podrán almacenar objetos Product, lo siguiente chequea que el numero de objetos Product,
        /// si no hay objetos, entonces la DB es llenada con una collecion de objetos Product usando el mth. AddRange() y guardando con SaveChanges()
        /// Lo último es servir a la DB cuando la app starts, que se hace con la llamada al mth `EnsurePopulated()` desde la clase Startup.
        /// </remarks>
        /// <param name="app">The application.</param>
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 19.50m
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer",
                        Price = 34.95m
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Category = "Soccer",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess",
                        Price = 1200
                    }
             );
                context.SaveChanges();
            }
        }
    }
    #endregion
}
