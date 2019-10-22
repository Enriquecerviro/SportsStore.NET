using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="quantity">The quantity.</param>
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        /// <summary>
        /// Removes the line.
        /// </summary>
        /// <param name="product">The product.</param>
        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        /// <summary>
        /// Computes the total value.
        /// </summary>
        /// <returns></returns>
        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);
        /// <summary>
        /// Clears this instance.
        /// </summary>
        public virtual void Clear() => lineCollection.Clear();
        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    /// <summary>
    /// 
    /// </summary>
    public class CartLine
    {
        /// <summary>
        /// Gets or sets the cart line identifier.
        /// </summary>
        /// <value>
        /// The cart line identifier.
        /// </value>
        public int CartLineID { get; set; }
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
    }
}
