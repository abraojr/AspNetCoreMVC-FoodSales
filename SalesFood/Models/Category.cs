namespace SalesFood.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Food> Foods { get; set; }
}

