
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SportsStore.Models
{
    #region ApplicationDbContext : DbContext    
    /// <summary>
    /// La clase `database context class` es el puente entre la app y EFC y proveé acceso
    /// a los datos persistidos de la app usando objetos del modelo(Producto en este caso(mayormente))
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        /// <summary>
        /// Gets or sets the products. Propiedad que nos permite acceder a los datos persistidos.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public DbSet<Order> Orders { get; set; }
    }
    #endregion
}
