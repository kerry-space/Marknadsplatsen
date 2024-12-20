using Marknadsplatsen.Models;

namespace Marknadsplatsen.ViewModels;

public class ListingCreateVm
{
    public required Listing Listing { get; set; }

    public List<Category> Categories { get; set; } = [];
}