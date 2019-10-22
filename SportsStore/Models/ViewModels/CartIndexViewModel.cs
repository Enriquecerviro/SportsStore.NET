using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CartIndexViewModel
    {
        /// <summary>
        /// Gets or sets the cart.
        /// </summary>
        /// <value>
        /// The cart.
        /// </value>
        public Cart Cart { get; set; }
        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string ReturnUrl { get; set; }
    }
}
