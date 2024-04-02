using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using minishop.Commons;
using minishop.Data;
using minishop.Models;
using minishop.Repositories;
using minishop.Repositories.DTOs.Conditions;


namespace minishop.test;
//TODO: unfinished 
public class testcaseone
{
  private readonly IMapper _mapper;

  [Fact]
  public async Task OrderInsertOrderIdAndOrderItemOrderIdMustMatcAsync()
  {

    Guid dbGuid = Guid.NewGuid();
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(dbGuid.ToString());

    var context = new ApplicationDbContext(optionsBuilder.Options);
    var mockedILogger = new Mock<ILogger<OrderRepository>>();

    var orderRepository = new OrderRepository(context, _mapper, mockedILogger.Object);

    OrderItem orderItem = new OrderItem
    {
      Id = 0,
      ProductId = 1,
      Price = 10m,
      Quantity = 1,
      ProductName = "Product 1",
      Order = new Order(),
      OrderId = Guid.NewGuid()
    };

    var condition = new OrderCondition
    {
      UserId = Guid.NewGuid().ToString(),
      Name = "Name",
      ReceiverName = "ReceiverName",
      ReceiverAddress = "123 Main St",
      ReceiverPhone = "0975199999",
      Status = OrderStatus.Pending,
      CreateAt = DateTime.UtcNow,
      OrderItems = new List<OrderItem> { orderItem }
    };

    string orderId = await orderRepository.Insert(condition);
    var insertedOrder = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id.ToString() == orderId);

    Assert.NotNull(insertedOrder);
    Assert.All(insertedOrder.OrderItems, item => Assert.Equal(insertedOrder.Id, item.OrderId));
  }
}

