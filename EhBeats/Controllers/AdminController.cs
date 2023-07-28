using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EhBeats.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MakeAdmin(string? email)
        {
            if (email == null)
            {
                return NotFound();
            }

            IdentityUser? user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction(nameof(PlaylistsController.MyPlaylists), nameof(PlaylistsController).Replace("Controller", ""));
        }
    }
}
