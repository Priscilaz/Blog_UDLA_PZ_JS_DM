
using BlogUDLA.AccesoDatos.Data.Repository.IRepository;
using BlogUDLA.Models;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                //Logica para guardar en BD
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
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
