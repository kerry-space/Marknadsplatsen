using Marknadsplatsen.Models;
using Microsoft.AspNetCore.Identity;

namespace Marknadsplatsen.ViewModels;

public class AdministrationIndexVm
{
    public List<IdentityUser> Users {get; set;} = [];
}