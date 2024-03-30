using minishop.Commons;

namespace minishop.Repositories.DTOs.Conditions;

public class ProductCondition
{
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public DateTime CreateAt { get; set; }
  public string CreateBy { get; set; } = null!;
  public DateTime? UpdateAt { get; set; }
  public ProductStatus Status { get; set; }
  public string UserId { get; set; } = null!;
}
