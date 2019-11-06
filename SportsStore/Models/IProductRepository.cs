using System.Linq;

namespace SportsStore.Models
{
    #region Interfaz RepositorioProducto

    /// <summary>
    /// Usamos IQueryable para permitir una llamada que obtenga una secuencia
    /// de objetos Producto, de esta manera una clase que depende de esta interfaz
    /// puede obtener objetos Producto sin la necesidad de saber como están guardados
    /// o como la clase implementada para a servirlos.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);
    }

    #endregion Interfaz RepositorioProducto
}