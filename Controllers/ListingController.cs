using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Marknadsplatsen.Models;
using Marknadsplatsen.ViewModels;


namespace Marknadsplatsen.Controllers;

public class ListingController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
{
    // GET: Listings
    public async Task<IActionResult> Index()
    {
        var currentUserId = userManager.GetUserId(User);
        var listings = await context.Listings
            .Where(l => l.OwnerId == currentUserId || User.IsInRole("Administrator"))
            .ToListAsync();

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync(ListingCreateVm listingVm)
    {

        var currentUser = await userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return Unauthorized();
        }
        listingVm.Listing.OwnerId = currentUser.Id;

        if (ModelState.IsValid)
        {
            context.Add(listingVm.Listing);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        listingVm.Categories = context.Categories.ToList();

        return View(listingVm);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var listing = await context.Listings.Include(l => l.Category)
            .FirstOrDefaultAsync(l => l.Id == id);


        if (listing == null)
        {
            return NotFound();
        }
        var categories = await context.Categories.ToListAsync();
        var currentUser = await userManager.GetUserAsync(User);
        listing.OwnerId = currentUser?.Id;

        var vm = new ListingEditVm
        {
            Listing = listing,
            Categories = categories
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> EditAsync(ListingEditVm listingVm)
    {
        if (ModelState.IsValid)
        {
            var listing = listingVm.Listing;

            var currentUser = await userManager.GetUserAsync(User);
            listing.OwnerId = currentUser?.Id;

            if (listing.CategoryId != null)
            {
                var category = await context.Categories.FindAsync(listing.CategoryId);
                if (category == null)
                {
                    ModelState.AddModelError("Listing.CategoryId", "The selected category is invalid.");
                    listingVm.Categories = await context.Categories.ToListAsync();
                    return View(listingVm);
                }
            }
            context.Attach(listing).State = EntityState.Modified;

            context.Update(listing);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        listingVm.Categories = await context.Categories.ToListAsync();
        return View(listingVm);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var isAdmin = User.IsInRole(RoleConstants.Administrator);

        var listing = await context.Listings.FirstOrDefaultAsync(l => l.Id == id);
        if (listing == null || (listing.OwnerId != userManager.GetUserId(User) && !isAdmin))
        {
            return Forbid();
        }

        var vm = new ListingDeleteVm
        {
            Listing = listing
        };

        return View(vm);
    }

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

