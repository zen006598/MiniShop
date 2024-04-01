using minishop.Commons;

namespace minishop.Repositories.DTOs.Conditions;

public class OrderSearchCondition
{
  public Guid? Id { get; set; }
  public string? Name { get; set; }
  public string? ReceiverPhone { get; set; }
  public OrderStatus? Status { get; set; }
}
