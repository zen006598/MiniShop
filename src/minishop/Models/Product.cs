using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using minishop.Commons;

namespace minishop.Models;

public class Product
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  [Range(0, 99999)]
  public int Quantity { get; set; } = 0;
  [Range(0, 99999)]
  public decimal Price { get; set; }
  public DateTime CreateAt { get; set; }
  [Required]
  public string CreateBy { get; set; } = null!;
  public DateTime? UpdateAt { get; set; }
  public ProductStatus Status { get; set; } = ProductStatus.Pending;
  public string UserId { get; set; } = null!;
  public ICollection<ApplicationUser> ApplicationUser { get; set; } = null!;

}