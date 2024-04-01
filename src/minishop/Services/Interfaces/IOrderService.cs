using minishop.Models;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;

namespace minishop.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderResultModel>> SearchOrdersAsync(OrderSearchInfo info);
    Task<IEnumerable<OrderResultModel>> ListOrdersWithUserAsync(string userId);
    Task<OrderResultModel> GetAsync(Guid id);
    Task<OrderResultModel> GetOrderWithUserAsync(Guid id, string userId);
    Task<string> Insert(OrderInfo info, ApplicationUser user);
    bool Update(Guid id, OrderInfo info);
    Task<bool> Cancel(Guid id);
}
