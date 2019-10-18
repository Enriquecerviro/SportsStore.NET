using System;
namespace SportsStore.Models.ViewModels
{
    #region PaginInfo Model    
    /// <summary>
    /// Esta `VistaModelo` ayuda al tagHelper(el que crea links para ir de una pag a otra) pasando informacion sobre el nº pags disponibles,la pag actual y el total de productos en el repo,
    /// para ello la manera más fácil es crear esta `VISTAMODELO` que pasa info entre el controller y la vista.
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        /// <value>
        /// The total items.
        /// </value>
        public int TotalItems { get; set; }
        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int ItemsPerPage { get; set; }
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Gets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages =>
        (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
    #endregion
}