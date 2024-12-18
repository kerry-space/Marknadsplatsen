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
}