using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;
// using Marknadsplatsen.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Marknadsplatsen.Controllers;

public class UserController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var users = await context.Users.ToListAsync();

        var vm = new UserIndexVm { Users = users };
        return View(vm);
    }

    public IActionResult Create()
    {
        var vm = new UserCreateVm
        {
            User = new User()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> CreateAsync(UserCreateVm userVm)
    {
        if (ModelState.IsValid)
        {
            context.Add(userVm.User);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userVm);
    }

    public async Task<IActionResult> EditAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var vm = new UserEditVm
        {
            User = user
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Edit(UserEditVm userVm)
    {
        var user = userVm.User;

        if (ModelState.IsValid)
        {
            if (!UserExists(user.Id))
            {
                return NotFound();
            }

            context.Update(user);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userVm);
    }


    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        var vm = new UserDeleteVm
        {
            User = user
        };

        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool UserExists(int id)
    {
        return context.Users.Any(user => user.Id == id);
    }

}

