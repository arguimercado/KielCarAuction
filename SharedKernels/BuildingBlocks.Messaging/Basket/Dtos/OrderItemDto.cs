namespace Order.Application.Orders.Dtos;

public record OrderItemEventDto(Guid ProductId,string ProductName,int Quantity,decimal Price);

