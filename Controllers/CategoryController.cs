using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;
// using Marknadsplatsen.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Marknadsplatsen.Controllers;

public class CategoryController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var categories = await context.Categories.ToListAsync();

        var vm = new CategoryIndexVm { Categories = categories };
        return View(vm);
    }

    // Show listings for a specific category
    public async Task<IActionResult> Listings(int id)
    {
        var category = await context.Categories
        .Include(c => c.Listings)
        .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var vm = new ListingIndexVm { Listings = category.Listings };

        return View(vm);
    }

    public IActionResult Create()
    {
        var vm = new CategoryCreateVm
        {
            Category = new Category()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> CreateAsync(CategoryCreateVm categoryVm)
    {
        context.Add(categoryVm.Category);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> EditAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        var vm = new CategoryEditVm
        {
            Category = category
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Edit(CategoryEditVm categoryVm)
    {
        var category = categoryVm.Category;

        if (ModelState.IsValid)
        {
            if (!CategoryExists(category.Id))
            {
                return NotFound();
            }
            context.Update(category);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoryVm);
    }

    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var category = await context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        var vm = new CategoryDeleteVm
        {
            Category = category
        };

        return View(vm);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        var listingsToRemove = context.Listings.Where(l => l.CategoryId == id).ToList();
        context.Listings.RemoveRange(listingsToRemove);
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return context.Categories.Any(e => e.Id == id);
    }
}
