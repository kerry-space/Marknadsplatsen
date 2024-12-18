using System.ComponentModel.DataAnnotations;

namespace Marknadsplatsen.Models;

public class Listing
{
    public int Id { get; set; }

    [Required]
    [MinLength(8, ErrorMessage="Title needs to be 8 characters.")] 
    public string Title { get; set; } = "";

    [Required]
    [MinLength(10, ErrorMessage = "Your description needs to be at least 10 characters.")] 
    public string Description { get; set; } = "";
    
    [Required (ErrorMessage = "You need to set a price.")] 
    public int Price { get; set; }
}
