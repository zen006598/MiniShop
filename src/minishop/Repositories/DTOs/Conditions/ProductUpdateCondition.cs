using minishop.Commons;

namespace minishop.Repositories.DTOs.Conditions;

public class ProductUpdateCondition
{
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public DateTime UpdateAt { get; set; }
  public ProductStatus Status { get; set; }
}
