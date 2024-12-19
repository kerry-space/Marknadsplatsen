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

    // GET: Listings/Create
    public IActionResult Create()
    {
        var vm = new ListingCreateVm
        {
            Listing = new Listing()
        };
        return View(vm);
    }

    // POST: Listings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            context.Add(listing);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(listing);
    }

    // GET: Listings/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var listing = await context.Listings.FindAsync(id);
        if (listing == null)
        {
            return NotFound();
        }
        return View(listing);
    }

    // POST: Listings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Listing listing)
    {
        if (id != listing.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(listing);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(listing);
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
}
