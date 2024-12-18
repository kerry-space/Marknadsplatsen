using System.ComponentModel.DataAnnotations;

namespace Marknadsplatsen.Models;


public class User
{
    
    public int Id {get; set;}

    [Required]
    [MinLength(5, ErrorMessage = "Ange minst 5 tecken!")]
    public string Name {get; set;} = "";
    public string Email{get; set;} = "";

}
