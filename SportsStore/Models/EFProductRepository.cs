using System.Linq;

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

        /// <summary>
        /// Saves the product.
        /// Añade el producto al repositorio si el ProductId ==0, de la otra manera no hace nada
        /// Sé que necesito hacer una update cuando reciba un objeto con ProductId !=0, y lo hago
        /// cogiendo un objeto Product del repo con el mismo ID y actualizando las propiedades
        /// individualmente para que así coincidan.Esto se consigue porque EFCore sigue la traza de los
        /// objetos que se crearon desde la DB. El objeto pasado al mth. SaveChanges es creado por
        /// el "MVC model binding feature" así que EFC no sabe nada sobre el nuevo objeto Product así
        /// que no va a realizar una update a la DB. Para solucionarlo vamos a ir a lo fácil y 
        /// localizamos el objeto que EFC si sabe algo y lo actualizamos explícitamente
        /// </summary>
        /// <param name="product">The product.</param>
        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if( dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }
    }

    #endregion EFProductRepository : IProductRepository
}