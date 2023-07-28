using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EhBeats.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private const string ADMIN_ROLE = "Admin";

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> MakeAdmin(string? email)
        {
            if (email == null)
            {
                return NotFound();
            }

            if (!await _roleManager.RoleExistsAsync(ADMIN_ROLE))
            {
                await _roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
            }

            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                await _userManager.AddToRoleAsync(user, ADMIN_ROLE);
            }

            return RedirectToAction(nameof(PlaylistsController.MyPlaylists), nameof(PlaylistsController).Replace("Controller", ""));
        }
    }
}
