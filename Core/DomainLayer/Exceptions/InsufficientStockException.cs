namespace DomainLayer.Exceptions
{
    public sealed class InsufficientStockException(string productName)
    : Exception($"Insufficient stock for product {productName}")
    {
    }
}
