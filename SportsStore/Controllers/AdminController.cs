using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    /// <summary>
    ///  The controller defines a single action method, Index, that calls the View
    ///  method to select the default view for the action, passing the set of products
    ///  in the database as the view model.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AdminController : Controller
    {
        private IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ViewResult Index() => View(repository.Products);

        /// <summary>
        /// Edits the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        public ViewResult Edit(int productId) =>
            View(repository.Products
                .FirstOrDefault(p => p.ProductID == productId));

        /// <summary>
        /// Edits the specified product.
        /// 
        /// uso TempData y no ViewBag porque VB pasa info entre la vista y el controlador
        /// y no puede mantener los datos más de lo que dura la petición HTTP, de ahí TempData que
        /// persiste hasta que se lea.Leeré esa info en la vista pintada por el action method al
        /// que redirecciono.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} ha sido modificado";
                return RedirectToAction("Index");
            }
            else
            {
                //cuando hay algun problema con los datos introducidos
                return View(product);
            }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} ha sido borrado";
            }
            return RedirectToAction("Index");
        }
    }
}
