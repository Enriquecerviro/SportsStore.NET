using System;
using System.Collections.Generic;
using SportsStore.Models;
namespace SportsStore.Models.ViewModels
{
    #region VISTAMODELO de ProducList
    /// <summary>
    /// Para usar la clase TagHelper(PageLinkTH)  necesito proveer a la vista una instancia
    /// de "PagingInfo" . Para hacer esto voy a envolver todo los datos que voy a enviar desde el controlador
    /// hasta la vista en solo una "view model class". Para hacer esto me creo esta clase-vista-modelo.
    /// </summary>
    public class ProductsListViewModel
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the paging information.
        /// </summary>
        /// <value>
        /// The paging information.
        /// </value>
        public PagingInfo PagingInfo { get; set; }
        /// <summary>
        /// Gets or sets the current category.
        /// </summary>
        /// <value>
        /// The current category.
        /// </value>
        public string CurrentCategory { get; set; }

    }
    #endregion
}
