namespace Order.Application.Orders.Dtos;


public record SalesOrderRequestEventDto(
    string UserName,
    Guid CustomerId,
    decimal TotalPrice,
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string City,
    string State,
    string ZipCode,
    string CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod);

