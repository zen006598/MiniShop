using minishop.Commons;
using minishop.Models;

namespace minishop.Services.DTOs.ResultModel;

public class ProductResultModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; }
    public DateTime CreateAt { get; set; }
    public string CreateBy { get; set; } = null!;
    public DateTime? UpdateAt { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Pending;
    public string UserId { get; set; } = null!;
    public ICollection<ApplicationUser> ApplicationUser { get; set; } = null!;
}
