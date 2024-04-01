using AutoMapper;
using minishop.Commons;
using minishop.Models;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;
using minishop.Repositories.Interfaces;
using minishop.Services.DTOs.Info;
using minishop.Services.DTOs.ResultModel;
using minishop.Services.Interfaces;

namespace minishop.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _order;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly IShoppingCartService _shoppingCart;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<OrderService> logger,
        IShoppingCartService shoppingCartService)
    {
        _order = orderRepository;
        _mapper = mapper;
        _logger = logger;
        _shoppingCart = shoppingCartService;
    }

    public async Task<bool> Cancel(Guid id)
    {
        return await _order.Cancel(id);
    }

    public async Task<OrderResultModel> GetAsync(Guid id)
    {
        var order = await _order.GetAsync(id);
        return _mapper.Map<OrderResultModel>(order);
    }


    public async Task<OrderResultModel> GetOrderWithUserAsync(Guid id, string userId)
    {
        var order = await _order.GetOrderWithUserAsync(id, userId);
        return _mapper.Map<OrderResultModel>(order);
    }

    public async Task<string> Insert(OrderInfo info, ApplicationUser user)
    {
        List<ShoppingCartItem> cartItems = _shoppingCart.GetCartItems();
        if (cartItems is null)
        {
            _logger.LogError("Shopping cart is empty in session.");
            return "";
        }

        var order = _mapper.Map<OrderInfo, OrderCondition>(info);
        order.CreateAt = DateTime.UtcNow;
        order.UserId = user.Id;
        order.Status = OrderStatus.Pending;
        order.OrderItems = _mapper.Map<List<OrderItem>>(cartItems);

        return await _order.Insert(order);
    }

    public async Task<IEnumerable<OrderResultModel>> SearchOrdersAsync(OrderSearchInfo info)
    {
        var condition = _mapper.Map<OrderSearchInfo, OrderSearchCondition>(info);
        var orders = await _order.SearchOrdersAsync(condition);
        var result = _mapper.Map<
            IEnumerable<OrderDataModel>,
            IEnumerable<OrderResultModel>>(orders);

        return result;
    }

    public async Task<IEnumerable<OrderResultModel>> ListOrdersWithUserAsync(string userId)
    {

        var orders = await _order.ListOrdersWithUserAsync(userId);
        var orderResultModel = _mapper.Map<
            IEnumerable<OrderDataModel>,
            IEnumerable<OrderResultModel>>(orders);

        return orderResultModel;
    }

    public bool Update(Guid id, OrderInfo info)
    {
        var order = _mapper.Map<OrderInfo, OrderCondition>(info);
        return _order.Update(id, order);
    }
}
