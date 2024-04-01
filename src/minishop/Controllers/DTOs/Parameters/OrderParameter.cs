using minishop.Commons;

namespace minishop.Controllers.DTOs.Parameters;

public class OrderParameter
{
  public Guid? Id { get; set; }
  public string? UserId { get; set; }
  public string? Name { get; set; }
  public string? ReceiverPhone { get; set; }
  public OrderStatus? Status { get; set; }
}
