using System.ComponentModel.DataAnnotations.Schema;

namespace SalesFood.Models;

public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int FoodId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public virtual Order Order { get; set; }
    public virtual Food Food { get; set; }
}