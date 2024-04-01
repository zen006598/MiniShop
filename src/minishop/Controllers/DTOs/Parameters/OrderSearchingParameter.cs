using minishop.Commons;
using minishop.Controllers.DTOs.ViewModels;

namespace minishop.Controllers.DTOs.Parameters;

public class OrderSearchingParameter
{
    public Guid? Id { get; set; }
    public string? UserId { get; set; }
    public string? Name { get; set; }
    public string? ReceiverPhone { get; set; }
    public OrderStatus? Status { get; set; }
    public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
}
