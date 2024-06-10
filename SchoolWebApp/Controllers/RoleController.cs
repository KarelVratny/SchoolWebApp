using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApp.Models;

namespace SchoolWebApp.Controllers {
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller {
		private RoleManager<IdentityRole> _roleManager;
		private UserManager<AppUser> _userManager;

		public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public IActionResult Index() {
			return View(_roleManager.Roles);
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(string name) {
			if (ModelState.IsValid) {
				IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
				if (result.Succeeded) {
					return RedirectToAction("Index");
				}
				else {
					AddErrors(result);
				}
			}
			return View(name);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAsync(string id) {
			var roleToDelete = await _roleManager.FindByIdAsync(id);
			if (roleToDelete != null) {
				IdentityResult result = await _roleManager.DeleteAsync(roleToDelete);
				if (result.Succeeded) {
					return RedirectToAction("Index");
				}
				else {
					AddErrors(result);
				}
			}
			ModelState.AddModelError("", "Role not found");
			return View("Index", _roleManager.Roles);
		}
		public async Task<IActionResult> Update(string id) {
			var role = await _roleManager.FindByIdAsync(id);
			List<AppUser> members = new List<AppUser>();
			List<AppUser> nonmembers = new List<AppUser>();
			foreach (AppUser user in _userManager.Users) {
				var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
				list.Add(user);
			}
			return View(new RoleEdit {
				Role = role,
				RoleMembers = members,
				RoleNonMembers = nonmembers
			});
		}

		[HttpPost]
		public async Task<IActionResult> Update(RoleModifications roleModifications) {
			foreach (var id in roleModifications.AddIds ?? new string[] { }) {
				AppUser user = await _userManager.FindByIdAsync(id);
				if (user != null) {
					IdentityResult result = await _userManager.AddToRoleAsync(user, roleModifications.RoleName);
					if (result != IdentityResult.Success) {
						AddErrors(result);
					}
				}
			}
			foreach (var id in roleModifications.DeleteIds ?? new string[] { }) {
				AppUser user = await _userManager.FindByIdAsync(id);
				if (user != null) {
					IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleModifications.RoleName);
					if (result != IdentityResult.Success) {
						AddErrors(result);
					}
				}
			}
			return RedirectToAction("Index");
		}
		private void AddErrors(IdentityResult identityResult) {
			foreach (var error in identityResult.Errors) {
				ModelState.AddModelError("", error.Description);
			}
		}
	}
}
