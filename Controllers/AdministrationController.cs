using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Marknadsplatsen.Controllers;

public class AdministrationController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        var users = await userManager.Users.ToListAsync();
        var vm = new AdministrationIndexVm
        {
            Users = users
        };
        return View(vm);
    }

    // GET: Administration/EditUser/5
    public async Task<IActionResult> EditUser(string id)
    {
        // Retrieve the user using the ID
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Create a ViewModel for editing the user
        var vm = new AdministrationEditUserVm
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return View(vm);
    }

    // POST: Administration/EditUser/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(AdministrationEditUserVm vm)
    {
        if (ModelState.IsValid)
        {
            // Find the user to edit
            var user = await userManager.FindByIdAsync(vm.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Update the user properties
            user.UserName = vm.UserName;
            user.Email = vm.Email;

            // Save the changes
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index)); // Redirect back to the user list after successful update
            }

            // Handle any errors during the update process
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(vm);
    }

    // GET: Administration/DeleteUser/5
    public async Task<IActionResult> DeleteUser(string id)
    {
        // Retrieve the user based on the ID
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Create a ViewModel for the delete confirmation
        var vm = new AdministrationDeleteUserVm
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return View(vm);
    }

    // POST: Administration/DeleteUser/5
    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(string id)
    {
        // Retrieve the user based on the ID
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Delete the user
        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index)); // Redirect back to the user list after successful deletion
        }

        // Handle any errors during the deletion process
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(); // Return the view if there was an error
    }
}

