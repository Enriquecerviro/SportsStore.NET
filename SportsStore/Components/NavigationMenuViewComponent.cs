using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMenuViewComponent"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        /// <summary>
        /// Invokes this instance.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
                );
        }
    }
}
