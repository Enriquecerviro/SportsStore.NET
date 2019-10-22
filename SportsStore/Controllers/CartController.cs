using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infraestructure;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CartController : Controller
    {
        private IProductRepository repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        /// <summary>
        /// Indexes the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        /// <summary>
        /// Adds to cart.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /// <summary>
        /// Removes from cart.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <returns> cart</returns>
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        /// <summary>
        /// Saves the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
