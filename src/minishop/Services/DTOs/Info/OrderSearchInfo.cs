using minishop.Commons;

namespace minishop.Services.DTOs.Info;

public class OrderSearchInfo
{
    public Guid? Id { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? ReceiverPhone { get; set; }
    public OrderStatus? Status { get; set; }
}
