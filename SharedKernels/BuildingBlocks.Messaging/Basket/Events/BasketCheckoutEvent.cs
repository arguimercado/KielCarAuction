using BuildingBlocks.Messaging.Contracts;
using Order.Application.Orders.Dtos;

namespace BuildingBlocks.Messaging.Basket.Events;

public record BasketCheckoutEvent : IntegrationEvent
{
    public SalesOrderRequestEventDto OrderEvent { get; set; } = default!;

    public IEnumerable<OrderItemEventDto> OrderItems { get; set; } = default!;


}
