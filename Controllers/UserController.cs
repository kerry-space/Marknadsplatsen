using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;
// using Marknadsplatsen.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Marknadsplatsen.Controllers;

public class AccountUserController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var accountUsers = await context.AccountUsers.ToListAsync();

        var vm = new AccountUserIndexVm { AccountUser = accountUsers };
        return View(vm);
    }

    public IActionResult Create()
    {
        var vm = new AccountUserCreateVm
        {
            AccountUser = new AccountUser()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> CreateAsync(AccountUserCreateVm userVm)
    {
        if (ModelState.IsValid)
        {
            context.Add(userVm.AccountUser);
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

        var user = await context.AccountUsers.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var vm = new AccountUserEditVm
        {
            AccountUser = user
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Edit(AccountUserEditVm userVm)
    {
        var user = userVm.AccountUser;

        if (ModelState.IsValid)
        {
            if (!AccountUserExists(user.Id))
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

        var user = await context.AccountUsers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        var vm = new AccountUserDeleteVm
        {
            AccountUser = user
        };

        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await context.AccountUsers.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        context.AccountUsers.Remove(user);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool AccountUserExists(int id)
    {
        return context.AccountUsers.Any(user => user.Id == id);
    }

}

