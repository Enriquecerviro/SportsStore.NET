using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    #region Clase Product

    /// <summary>
    /// Como es un e-commerce, el modelo más importante será esta clase
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Introduce un nombre de producto correcto")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage ="Porfavor introduzca descripción. ")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Required]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "Porfavor introduzca un numero positivo")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Required(ErrorMessage ="Porfavor especifíca una categoría.")]
        public string Category { get; set; }
    }

    #endregion Clase Product
}