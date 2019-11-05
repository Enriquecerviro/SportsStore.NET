using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

#region Clase Startup

/// <summary>
///
/// </summary>
public class Startup
{
    /// <summary>
    /// Este constructor me permite acceder a la string que conecta a la base de datos, asi usare configuration para
    /// recibir la carga de datos desde appsetting.json.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Cuando la app bootstrapee la configuracion de la app le dice a MVC lo que necesita para crear un controlador de Producto
    /// que permita manejar las peticiones. Crear un nuevo controlador de Product significa invocar al contructor `ProductControler`
    /// que a su vez requiere un objeto del tipo interfaz IProductRepository, y la configuracion le dice a MVC
    /// que un objeto EFProductRepository se creará y se usará para la petición. El EFProductRepository
    /// </para>
    /// </remarks>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public IConfiguration Configuration { get; }

    #region Startup ConfigureServices

    /// <summary>
    /// "Loosely coupled components" significa que puedes hacer cambios en una parte
    /// de la app sin tener que hacer los cambios correspondientes en otra parte de la app,
    /// este approach hace que los denominemos servicios, que nos proveen de funciones que necesitan
    /// varias partes de la app. La clase que produce el servicio puede ser alterada
    /// sin tener que cambiar la clase que consume ese servicio.
    /// Este mth. tb lee el string connection y configura la app para conectarse a la DB.(El trabajo de leer JSON es de la clase Program.
    /// </summary>
    /// <remarks>
    /// El método extendido `AddDbContext`(propio de IServiceCollection) prepara los servicios de EFC para la clase.
    /// El argumento para el método `AddDbContext` es una expresion lambda que recibe un objeto de opciones que
    /// configura la clase del contexto DB. En este caso configuré la DB con el mth. UseSqlServer y especifiqué una string de
    /// conexion proveída por la propiedad Configuration.
    /// </remarks>
    /// <param name="services">The services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<IProductRepository, EFProductRepository>();

        services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IOrderRepository, EFOrderRepository>();
        services.AddMvc();
        services.AddMemoryCache();
        services.AddSession();
    }

    #endregion Startup ConfigureServices

    #region Startup Configure

    /// <summary>
    /// Configures the specified application. Este método se usa para preparar las tuberias solicitadas,
    /// que consisten en clases(middleware) que inspeccionan las pet.HTTP y generan respuestas.
    /// El mth. UseEndpoints prepara el middleware de MVC, y una de sus configuraciones es el esquema que va
    /// a usar para mapear las URLs hacia los controladores y los action methods.
    /// </summary>
    /// <remarks>
    /// El mth. EnsurePopulated(app) es lo que nos permite servir de datos la DB usando la clase SeedData.
    /// </remarks>
    /// <param name="app">The application.</param>
    /// <param name="env">The env.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStatusCodePages();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: null,
                pattern: "{category}/Page{productPage:int}",
                defaults: new { controller = "Product", action = "List" }
            );
            endpoints.MapControllerRoute(
                name: null,
                pattern: "Page{productPage:int}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
            );
            endpoints.MapControllerRoute(
                name: null,
                pattern: "{category}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
            );
            endpoints.MapControllerRoute(
                name: null,
                pattern: "",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                    productPage = 1
                }
            );
            endpoints.MapControllerRoute("default", "{controller=Product}/{action=List}/{id?}");
        });
        SeedData.EnsurePopulated(app);
    }

    #endregion Startup Configure
}

#endregion Clase Startup