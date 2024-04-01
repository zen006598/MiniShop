using minishop.Commons;

namespace minishop.Controllers.DTOs.ViewModels;

public class OrderSearchingViewModel
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? ReceiverPhone { get; set; }
    public OrderStatus? Status { get; set; }
    public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
}
