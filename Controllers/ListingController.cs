using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;

namespace Marknadsplatsen.Controllers;

public class ListingController(ApplicationDbContext context) : Controller
{
    // GET: Listings
    public async Task<IActionResult> Index()
    {
        var listings = await context.Listings.ToListAsync();
        var vm = new ListingIndexVm { Listings = listings };
        return View(vm);
    }

    // GET: Listings
    public async Task<IActionResult> FilterById(int? id)
    {
        var listings = await context.Listings.ToListAsync();
        var vm = new ListingIndexVm { Listings = listings };
        return View(vm);
    }

    // GET: Listings/Create
    public IActionResult Create()
    {
        var categories = context.Categories.ToList();
        if (categories == null)
        {
            return NotFound();
        }

        var vm = new ListingCreateVm
        {
            Listing = new Listing(),
            Categories = categories
        };
        return View(vm);
    }

    // POST: Listings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(ListingCreateVm listingVm)
    {
        if (ModelState.IsValid)
        {
            context.Add(listingVm.Listing);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(listingVm);
    }

    public async Task<IActionResult> EditAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var listing = await context.Listings.FindAsync(id);

        if (listing == null)
        {
            return NotFound();
        }

        var vm = new ListingEditVm
        {
            Listing = listing
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Edit(ListingEditVm listingVm)
    {
        var listing = listingVm.Listing;

        if (ModelState.IsValid)
        {
            if (!CategoryExists(listing.Id))
            {
                return NotFound();
            }

            context.Update(listing);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(listingVm);
    }

    // GET: Listings/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == id);
        if (listing == null)
        {
            return NotFound();
        }

        var vm = new ListingDeleteVm
        {
            Listing = listing
        };

        return View(vm);
    }

    // POST: Listings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var listing = await context.Listings.FindAsync(id);
        if (listing == null)
        {
            return NotFound();
        }
        context.Listings.Remove(listing);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ListingExists(int id)

    {
        return context.Listings.Any(e => e.Id == id);
    }

    private bool CategoryExists(int id)
    {
        return context.Listings.Any(e => e.Id == id);
    }
}

