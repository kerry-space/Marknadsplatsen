using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Marknadsplatsen.ViewModels;
using Marknadsplatsen.Models;

namespace Marknadsplatsen.Controllers;

public class ListingController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var listings = await context.Listings.ToListAsync();


        var vm = new ListingIndexVm{ Listings = listings };
        return View(vm);
    }
}