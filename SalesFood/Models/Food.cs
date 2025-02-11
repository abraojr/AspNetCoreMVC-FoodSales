using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesFood.Models;

[Table("Foods")]
public class Food
{
    [Key]
    public int FoodId { get; set; }

    [Required(ErrorMessage = "Food name is required")]
    [Display(Name = "Food Name")]
    [StringLength(80, MinimumLength = 10, ErrorMessage = "The {0} must be at least {1} and at most {2} characters long")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Food description is required")]
    [Display(Name = "Food Description")]
    [MinLength(20, ErrorMessage = "Minimum length is {1} characters")]
    [MaxLength(200, ErrorMessage = "Maximum length is {1} characters")]
    public string ShortDescription { get; set; }

    [Required(ErrorMessage = "Food detailed description is required")]
    [Display(Name = "Detailed Description")]
    [MinLength(20, ErrorMessage = "Minimum length is {1} characters")]
    [MaxLength(200, ErrorMessage = "Maximum length is {1} characters")]
    public string LongDescription { get; set; }

    [Required(ErrorMessage = "Food price is required")]
    [Display(Name = "Price")]
    [Column(TypeName = "decimal(10, 2)")]
    [Range(1, 999.99, ErrorMessage = "The price must be between {1} and {2}")]
    public decimal Price { get; set; }

    [Display(Name = "Image Path")]
    [StringLength(200, ErrorMessage = "The {0} must have a maximum of {1} characters")]
    public string ImageUrl { get; set; }

    [Display(Name = "Thumbnail Image Path")]
    [StringLength(200, ErrorMessage = "The {0} must have a maximum of {1} characters")]
    public string ImageThumbnailUrl { get; set; }

    [Display(Name = "Favorite")]
    public bool IsFavoriteFood { get; set; }

    [Display(Name = "Stock")]
    public bool InStock { get; set; }

    [Display(Name = "Categories")]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

