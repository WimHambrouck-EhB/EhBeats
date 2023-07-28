using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EhBeats.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> MakeAdmin(string? email)
        {
            if (email == null)
            {
                return NotFound();
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            IdentityUser? user = await _userManager.FindByEmailAsync(email);

            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction(nameof(PlaylistsController.MyPlaylists), nameof(PlaylistsController).Replace("Controller", ""));
        }
    }
}
