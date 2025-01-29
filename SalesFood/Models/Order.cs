using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesFood.Models;

public class Order
{
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100)]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [StringLength(100)]
    [Display(Name = "Complement")]
    public string AddressComplement { get; set; }

    [Required(ErrorMessage = "Zip code is required")]
    [StringLength(10, MinimumLength = 8)]
    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; }

    [StringLength(10)]
    public string State { get; set; }

    [StringLength(50)]
    public string City { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(25)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email does not have a correct format")]
    public string Email { get; set; }

    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Total Order")]
    public decimal TotalOrder { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Order Items")]
    public int TotalOrderItems { get; set; }

    [Display(Name = "Order Date")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Delivery Date")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DeliveryDate { get; set; }

    public List<OrderDetail> OrderItems { get; set; }
}
