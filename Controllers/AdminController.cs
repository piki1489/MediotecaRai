using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediotecaRai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        //MOSTRAMOS LOS USUARIOS REGISTRADOS
        public IActionResult Usuarios()
        {
            var usuarios = _userManager.Users.ToList();
            return View(usuarios);
        }
        //ADMIN PUEDE BLOQUEAR O DESBLOQUEAR USUARIOS
        public async Task<IActionResult> Bloquear(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Usuarios));
        }

        public async Task<IActionResult> Desbloquear(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Usuarios));
        }
    }
}
