using System.ComponentModel.DataAnnotations;
using minishop.Commons;

namespace minishop.Controllers.DTOs.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Pending;
}
