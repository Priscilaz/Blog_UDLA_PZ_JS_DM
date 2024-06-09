using Blog_UDLA_PZ_JS_DM.Models;
using BlogUDLA.AccesoDatos.Data.Repository.IRepository;
using BlogUDLA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog_UDLA_PZ_JS_DM.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;
        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                ListArticulos = _contenedorTrabajo.Articulo.GetAll()
            };

            ViewBag.IsHome = true;
            return View(homeVM);
        }
        [HttpGet]
        public IActionResult Detalle(int id) 
        { 
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            return View(articuloDesdeBd);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
