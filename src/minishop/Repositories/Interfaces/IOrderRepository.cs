using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;

namespace minishop.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDataModel>> SearchOrdersAsync(OrderSearchCondition Condition);
    Task<OrderDataModel> GetAsync(Guid id);
    Task<string> Insert(OrderCondition condition);
    bool Update(Guid id, OrderCondition condition);
    Task<bool> Cancel(Guid id);
    Task<OrderDataModel> GetOrderWithUserAsync(Guid id, string userId);
    Task<IEnumerable<OrderDataModel>> ListOrdersWithUserAsync(string userId);
}
