using System.ComponentModel.DataAnnotations;

namespace SalesFood.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "User")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}