using minishop.Commons;
using minishop.Models;

namespace minishop.Controllers.DTOs.ViewModels;

public class OrderViewModel
{
  public Guid Id { get; set; }
  public string UserId { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string ReceiverName { get; set; } = null!;
  public string ReceiverAddress { get; set; } = null!;
  public string ReceiverPhone { get; set; } = null!;
  public decimal Amount => Math.Ceiling(Math.Round(OrderItems.Sum(item => item.SubTotal), 2));
  public decimal ReceivedAmount { get; set; }
  public List<OrderItem> OrderItems { get; set; } = null!;
  public OrderStatus Status { get; set; }
  public DateTime CreateAt { get; set; }
}
