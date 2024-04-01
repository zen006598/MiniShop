using AutoMapper;
using Microsoft.EntityFrameworkCore;
using minishop.Commons;
using minishop.Data;
using minishop.Models;
using minishop.Repositories.DTOs.Conditions;
using minishop.Repositories.DTOs.DataModel;
using minishop.Repositories.Interfaces;

namespace minishop.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderRepository> _logger;
    public OrderRepository(
        ApplicationDbContext dbContext,
        IMapper mapper,
        ILogger<OrderRepository> logger
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrderDataModel> GetAsync(Guid id)
    {
        try
        {
            var order = await _dbContext.Orders
                                .Include(o => o.OrderItems)
                                .FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<OrderDataModel>(order);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in OrderRepository GetAsync method", ex);
        }
    }

    public async Task<string> Insert(OrderCondition condition)
    {
        try
        {
            Order orderToInsert = _mapper.Map<Order>(condition);
            foreach (var item in orderToInsert.OrderItems)
            {
                item.Order = orderToInsert;
            }
            await _dbContext.Orders.AddAsync(orderToInsert);
            await _dbContext.SaveChangesAsync();
            return orderToInsert.Id.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}, {ex.InnerException}");
            return "";
        }
    }

    public async Task<IEnumerable<OrderDataModel>> SearchOrdersAsync(OrderSearchCondition condition)
    {
        var query = _dbContext.Orders.Include(o => o.OrderItems).AsQueryable();

        if (!string.IsNullOrEmpty(condition.Id.ToString()))
        {
            query = query.Where(o => o.Id == condition.Id);
        }

        if (!string.IsNullOrEmpty(condition.Name))
        {
            query = query.Where(o => o.Name.Contains(condition.Name));
        }

        if (!string.IsNullOrEmpty(condition.ReceiverPhone))
        {
            query = query.Where(o => o.ReceiverPhone == condition.ReceiverPhone);
        }

        if (condition.Status is not null)
        {
            query = query.Where(o => o.Status == condition.Status);
        }

        var filteredOrder = await query.ToListAsync();
        return _mapper.Map<IEnumerable<OrderDataModel>>(filteredOrder);
    }

    public bool Update(Guid id, OrderCondition condition)
    {
        try
        {
            var orderToUpdate = _dbContext.Orders.Find(id);
            if (orderToUpdate is null)
            {
                _logger.LogError($"Order with id: {id}, hasn't been found in the database.");
                return false;
            }
            _mapper.Map(condition, orderToUpdate);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}, {ex.InnerException}");
            return false;
        }
    }

    public async Task<bool> Cancel(Guid id)
    {
        try
        {
            var orderToCancel = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderToCancel is null)
            {
                _logger.LogError($"Order with id: {id}, hasn't been found in the database.");
                return false;
            }
            orderToCancel.Status = OrderStatus.Cancel;
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message}, {ex.InnerException}");
            return false;
        }
    }

    public async Task<OrderDataModel> GetOrderWithUserAsync(Guid id, string userId)
    {
        try
        {
            var order = await _dbContext.Orders
                                .Include(o => o.OrderItems)
                                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            return _mapper.Map<OrderDataModel>(order);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in OrderRepository GetItemsAsync method", ex);
        }
    }

    public async Task<IEnumerable<OrderDataModel>> ListOrdersWithUserAsync(string userId)
    {
        var orders = await _dbContext.Orders.Include(o => o.OrderItems).Where(o => o.UserId == userId).ToListAsync();
        return _mapper.Map<IEnumerable<OrderDataModel>>(orders);
    }
}
