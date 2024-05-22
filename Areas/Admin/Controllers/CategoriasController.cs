
using BlogUDLA.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Blog_UDLA_PZ_JS_DM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
           _contenedorTrabajo= contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        #region Llamadas a la APi
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll()});
        } 



        #endregion
    }
}
