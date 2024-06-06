using BlogUDLA.AccesoDatos.Data.Repository.IRepository;
using BlogUDLA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog_UDLA_PZ_JS_DM.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public ArticulosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo= new BlogUDLA.Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };
            return View(artiVM);
        }

        #region Llamadas a la APi
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        #endregion
    }
}
