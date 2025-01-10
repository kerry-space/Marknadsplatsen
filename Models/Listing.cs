using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Marknadsplatsen.Models;

public class Listing
{
    public int Id { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Title needs to be 8 characters.")]
    public string Title { get; set; } = "";

    [Required]
    [MinLength(10, ErrorMessage = "Your description needs to be at least 10 characters.")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "You need to set a price.")]
    public int Price { get; set; }

    //Foreign Key för category
    public int? CategoryId { get; set; }

    //Navigera properties 
    public Category? Category { get; set; }

    public string? OwnerId { get; set; } // Koppling till AspNetUser

    public IdentityUser? Owner { get; set; }

    public string? ImageUrl { get; set; }
}
