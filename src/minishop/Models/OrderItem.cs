using System.ComponentModel.DataAnnotations;

namespace minishop.Models;

public class OrderItem
{
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Range(0, 99999)]
    public decimal Price { get; set; }
    public decimal SubTotal => Math.Ceiling(Math.Round(Price * Quantity, 2));
    [Range(0, 99999)]
    public int Quantity { get; set; }
    public Order Order { get; set; } = null!;
    public Guid OrderId { get; set; }
    public string ProductName { get; set; } = null!;
}
