﻿using BlogUDLA.AccesoDatos.Data.Repository.IRepository;
using BlogUDLA.Models;
using BlogUDLA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog_UDLA_PZ_JS_DM.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;
       

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                
                if (archivos.Count() > 0)
                {
                    //Nuevo Slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);

                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }   
            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            if (id != null)
            {
                var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeBd = _contenedorTrabajo.Slider.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo imagen Slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeBd.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);

                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDesdeBd.UrlImagen;
                }

                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }



        #region Llamadas a la APi
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderDesdeBd = _contenedorTrabajo.Slider.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (sliderDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }
            _contenedorTrabajo.Slider.Remove(sliderDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider eliminado" });
        }


        #endregion
    }
}
