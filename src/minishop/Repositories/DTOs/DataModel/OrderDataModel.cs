using minishop.Commons;
using minishop.Models;

namespace minishop.Repositories.DTOs.DataModel;

public class OrderDataModel
{
  public Guid Id { get; set; }
  public string UserId { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string ReceiverName { get; set; } = null!;
  public string ReceiverAddress { get; set; } = null!;
  public string ReceiverPhone { get; set; } = null!;
  public decimal Amount => Math.Ceiling(Math.Round(OrderItems.Sum(item => item.SubTotal), 2));
  public decimal ReceivedAmount { get; set; }
  public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  public OrderStatus Status { get; set; }
  public DateTime CreateAt { get; set; }
}
