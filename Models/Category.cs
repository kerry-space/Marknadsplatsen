using System.ComponentModel.DataAnnotations;
namespace Marknadsplatsen.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a valid category name")]
    [Display(Name = "Category name:")]
    public string? Name { get; set; } = "";

    public List<Listing> Listings { get; set; } = [];

    public string? ImageUrl { get; set; }
}