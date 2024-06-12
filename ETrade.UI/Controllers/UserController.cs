 using Etrade.Data.Models.Identity;
using Etrade.Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.UI.Controllers
{
    [Authorize(Roles ="Admin,Moderator")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var users = new List<AppUser>();
            foreach (var item in admins)
            {
                users = _userManager.Users.Where(x => x.Id != item.Id).ToList();
            }
            return View(users);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> RoleAssign(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var roles = _roleManager.Roles.Where(x => x.Name != "Admin").ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            var RoleAssigns = new List<RoleAssignViewModel>();

            roles.ForEach(role => RoleAssigns.Add(new RoleAssignViewModel()
            {
                HasAssign = userRoles.Contains(role.Name),
                Id = role.Id,
                Name = role.Name
            }));
            ViewBag.Username = user.Name;
            return View(RoleAssigns);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> models, int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            foreach (var role in models)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.Name);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
                return RedirectToAction("Index");

            return NotFound("Silme işlemi başarısız!!!");
        }
    }
}
