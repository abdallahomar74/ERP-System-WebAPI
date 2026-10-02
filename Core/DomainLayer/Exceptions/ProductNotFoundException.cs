namespace DomainLayer.Exceptions
{
    public sealed class ProductNotFoundException(Guid id)
    : NotFoundExpceptions($"Product with Id = {id} not found")
    {
    }
}
