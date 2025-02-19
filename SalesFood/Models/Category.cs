using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesFood.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Category description is required")]
    [StringLength(200, ErrorMessage = "Maximum length is 200 characters")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    public List<Food> Foods { get; set; }
}