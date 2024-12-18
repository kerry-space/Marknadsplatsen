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

        var vm = new UserIndexVm { Users = users};
        return View(vm);
    }
}