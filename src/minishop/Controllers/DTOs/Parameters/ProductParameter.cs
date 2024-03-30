using System.ComponentModel.DataAnnotations;
using minishop.Commons;

namespace minishop.Controllers.DTOs.Parameters;

public class ProductParameter
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Range(0, 99999)]
    public int Quantity { get; set; } = 0;
    [Range(0, 99999)]
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Pending;
}
