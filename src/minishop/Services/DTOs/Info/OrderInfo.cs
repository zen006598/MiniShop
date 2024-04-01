using minishop.Models;

namespace minishop.Services.DTOs.Info;

public class OrderInfo
{
    public string Name { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;
    public string ReceiverAddress { get; set; } = null!;
    public string ReceiverPhone { get; set; } = null!;
}
