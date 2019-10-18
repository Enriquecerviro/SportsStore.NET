using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.ViewModels;


namespace SportsStore.Controllers
{
    #region ProductController Class    
    /// <summary>
    /// Cuando se necesite crear una instancia de esta clase para manejar una peticion HTTP
    /// va a ver que el constructor necesita un objeto que implementa la interfaz IProductRepository.
    /// Para saber la clase que debe implementar,MCV consulta la configuración en la clase Startup,
    /// que le dice que IProductRepository se usará y que se creará una nueva instancia cada vez que se llame.
    /// De esta manera MCV crea un nuevo objeto de IProductRepository que procesará la petición HTTP.
    /// Esto es DEPENDENCY INJECTION.
    /// </summary>
    /// <remarks>
    /// Este controlador usa la interfaz IProductRepository, recibirá un objeto EFProductRepository, que
    /// proveerá acceso a la DB.
    /// </remarks>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ProductController : Controller
    {
        /// <summary>
        /// The repository
        /// </summary>
        private IProductRepository repository;
        /// <summary>
        /// The page size. Este campo especifica que quiero 4 productos por página.
        /// </summary>
        public int PageSize = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Lists the specified category. He añadido un param adicional <paramref name="productPage"/> que obliga a cuando el 
        /// cuando llame al constructor sin parametros(List()), mi llamada va a ser tratada en verdad como si fuese
        /// List(1). El efecto conseguido es que cuando se invoque sin argumentos siempre renderize la primera página.
        /// Dentro del cuerpo del mth consigo los objetos Product, ordenados por la clave primaria, desechamos los objetos Producto
        /// que ocurren antes de la pagina actual y coje la cantidad de objetos especificados por el campo `PageSize`
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="productPage">The product page.</param>
        /// <returns></returns>
        public ViewResult List(string category ,int productPage = 1)
           => View(new ProductsListViewModel
           {
               Products = repository.Products
               .Where(p => category == null || p.Category==category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
               PagingInfo = new PagingInfo
               {
                   CurrentPage = productPage,
                   ItemsPerPage = PageSize,
                   TotalItems = repository.Products.Count()
               },
               CurrentCategory = category
           });

    }
    #endregion
}

