using minishop.Commons;

namespace minishop.Services.DTOs.Info;

public class ProductInfo
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
