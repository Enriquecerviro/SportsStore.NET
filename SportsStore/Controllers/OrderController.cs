using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="repoService">The repo service.</param>
        /// <param name="cartService">The cart service.</param>
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        
        /// <summary>
        /// Lists this instance. Selecciona todos los objetos Order en el repo que tienen el
        /// valor 'Shipped' en 'false' y los pasa a la vista por defecto. Este action method se
        /// usará para pintar una lista de 'unShipped' pedidos para el administrador. Para ello usaremos
        /// una vista Razor con un elemento `table` para pintar detalles de uno y otro, incluidos que productos
        /// han sido comprados.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult List() =>
            View(repository.Orders.Where(o => !o.Shipped));

        /// <summary>
        /// Marks the shipped. Recibe una peticion POST que especifica el ID de un pedido,
        /// que es usado para localizar el objeto Order corrrespondiente del repositorio de
        /// la forma que la propiedad 'Shipped' pueda ser seteada a true y guardada.
        /// <para>
        /// Para pintar la lista de unShipped orders, usare una vista razor con un elemento
        /// `table` para pintar algunos detalles, como qué productos se han comprado.
        /// </para>
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, el carrito está vacío!!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}