using MediotecaRai.Data;
using MediotecaRai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediotecaRai.Controllers
{
    public class MediaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MediaController(ApplicationDbContext context)
        {
            _context = context;
        }
        //FILTRO BÚSQUEDA
        public IActionResult Index(string tipo, string titulo)
        {
            var query = _context.MediaItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                query = query.Where(m => m.Tipo == tipo);
            }
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                //ponemos Contains para una busqueda más flexible
                query = query.Where(m => m.Titulo.Contains(titulo));
            }

            var resultados = query.ToList();
            return View(resultados);
        }
        //CREACION DE ITEMS 
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(MediaItem mediaItem)
        {
            if (string.IsNullOrWhiteSpace(mediaItem.Titulo))
            {
                TempData["Error"] = "El título es obligatorio";
                return View(mediaItem);
            }

            if (string.IsNullOrWhiteSpace(mediaItem.Tipo))
            {
                TempData["Error"] = "El tipo es obligatorio";
                return View(mediaItem);
            }

            if (string.IsNullOrWhiteSpace(mediaItem.Autor))
            {
                TempData["Error"] = "El autor es obligatorio";
                return View(mediaItem);
            }

            if (mediaItem.Anio == 0)
            {
                TempData["Error"] = "El año es obligatorio";
                return View(mediaItem);
            }

            if (string.IsNullOrWhiteSpace(mediaItem.Descripcion))
            {
                TempData["Error"] = "La descripción es obligatoria";
                return View(mediaItem);
            }

            _context.MediaItems.Add(mediaItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //RECOGER EL DETALLE DEL ITEM SELECCIONADO
        public IActionResult Detalles(int id)
        {
            var item = _context.MediaItems.FirstOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        //BORRADO DE ITEMS 
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var item = _context.MediaItems.FirstOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _context.MediaItems.Remove(item);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
