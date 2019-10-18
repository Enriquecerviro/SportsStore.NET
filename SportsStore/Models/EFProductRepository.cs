using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    #region EFProductRepository : IProductRepository    
    /// <summary>
    /// Esta clase implementa la interfaz IProductRepository y consigue los datos usando
    /// el Entity Framework Core . Este repositorio(EFProductRepository) lo que hace es
    /// mapear las propiedades Producto definida en el repositorio IProductRepository
    /// </summary>
    /// <seealso cref="SportsStore.Models.IProductRepository" />
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFProductRepository"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public IQueryable<Product> Products => context.Products;
    }
    #endregion
}
