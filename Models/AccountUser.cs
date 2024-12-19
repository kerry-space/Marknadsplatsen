using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace Marknadsplatsen.Models;


public class AccountUser
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Please enter a valid name")]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
    public string Name {get; set;} = "";

    [Required(ErrorMessage = "Please enter a valid email address")]
    [EmailAddress]
    public string Email{get; set;} = "";

    [Required(ErrorMessage = "Please enter a valid phone number")]
    public int PhoneNumber { get; set; } 

    [Required(ErrorMessage = "Please enter a valid country")]
    public string Country {get; set;} = "";

    public List<Listing> Listings {get; set;} = [];
}
