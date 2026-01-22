using MediotecaRai.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public IActionResult Index(string tipo, string titulo)
        {
            var query = _context.MediaItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                query = query.Where(m => m.Tipo == tipo); 
            }
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                query = query.Where(m => m.Titulo == titulo);
            }
            var resultados = query.ToList();
            return View(resultados);
        }
    }
}
