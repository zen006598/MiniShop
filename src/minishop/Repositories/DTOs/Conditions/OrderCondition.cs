using minishop.Commons;
using minishop.Models;

namespace minishop.Repositories.DTOs.Conditions;

public class OrderCondition
{
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;
    public string ReceiverAddress { get; set; } = null!;
    public string ReceiverPhone { get; set; } = null!;
    public decimal ReceivedAmount { get; set; } = 0;
    public OrderStatus Status { get; set; }
    public DateTime CreateAt { get; set; }
    public List<OrderItem> OrderItems { get; set; } = null!;
}
