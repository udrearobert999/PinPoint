using Application.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace PinPoint.Controllers
{
    [Authorize(Roles=("Admin"))]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> AdminPanel()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return View(new AdminPanelDto
            {
                Users = users.Select(u => u.UserName).ToList(),
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AdminPanelDto model)
        {
            if (model.SelectedRole.IsNullOrEmpty() || model.SelectedUser.IsNullOrEmpty())
            {
                TempData["CommentMessage"] = "User or role not selected";
                return RedirectToAction("AdminPanel");
            }

            var user = await _userManager.FindByNameAsync(model.SelectedUser);
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (result.Succeeded)
                {
                    TempData["CommentMessage"] = "User was successfully added to the role.";
                }
                else
                {
                    TempData["CommentMessage"] = "Failed to add user to the role.";
                }
            }
            else
            {
                TempData["CommentMessage"] = "User not found.";
            }

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(AdminPanelDto model)
        {
            if (model.SelectedRole.IsNullOrEmpty() || model.SelectedUser.IsNullOrEmpty())
            {
                TempData["CommentMessage"] = "User or role not selected";
                return RedirectToAction("AdminPanel");
            }

            var user = await _userManager.FindByNameAsync(model.SelectedUser);

            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, model.SelectedRole);
                if (result.Succeeded)
                {
                    TempData["CommentMessage"] = "User was successfully removed from the role.";
                }
                else
                {
                    TempData["CommentMessage"] = "Failed to remove user from the role.";
                }
            }
            else
            {
                TempData["CommentMessage"] = "User not found.";
            }

            return RedirectToAction("AdminPanel");
        }
    }
}
