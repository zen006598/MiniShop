using System.ComponentModel.DataAnnotations;
using minishop.Commons;

namespace minishop.Models;
public class Order
{
    public Guid Id { get; set; }
    [Required]
    public string? Name { get; set; } = null!;
    [Required]
    public string ReceiverName { get; set; } = null!;
    [Required]
    public string ReceiverAddress { get; set; } = null!;
    [Required]
    public string ReceiverPhone { get; set; } = null!;
    [Range(0, int.MaxValue)]
    public decimal Amount => Math.Ceiling(Math.Round(OrderItems.Sum(item => item.SubTotal), 2));
    public decimal ReceivedAmount { get; set; } = 0;
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public OrderStatus Status { get; set; } = OrderStatus.Processing;
    public string? UserId { get; set; } = null!;
    public ICollection<ApplicationUser> ApplicationUser { get; set; } = null!;
}