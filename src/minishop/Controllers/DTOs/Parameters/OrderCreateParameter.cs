using minishop.Models;

namespace minishop.Controllers.DTOs.Parameters;

public class OrderCreateParameter
{
    public string Name { get; set; } = null!;
    public string ReceiverName { get; set; } = null!;
    public string ReceiverAddress { get; set; } = null!;
    public string ReceiverPhone { get; set; } = null!;
}
