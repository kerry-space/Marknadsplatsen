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
        if (id == null)
        {
            return NotFound();
        }

        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

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
            var user = await userManager.FindByIdAsync(vm.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Update user information
            user.UserName = vm.UserName;
            user.Email = vm.Email;

            // Save changes to user
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index)); // Redirect to user list after update
            }

            // Handle errors during the update process
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(vm);
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var vm = new AdministrationDeleteUserVm
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
        };
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(AdministrationDeleteUserVm vm)
    {
        var user = await userManager.FindByIdAsync(vm.UserId);
        if (user == null)
        {
            return NotFound();
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UserListings(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return NotFound();
        }

        var UserListings = await context.Listings
            .Where(l => l.OwnerId == userId)
            .Include(l => l.Category)
            .ToListAsync();

        if (UserListings == null)
        {
            return NotFound();
        }

        var vm = new UserListingVm
        {
            Listings = UserListings
        };

        return View(vm);
    }

}

