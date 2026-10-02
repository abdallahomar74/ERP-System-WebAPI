namespace DomainLayer.Exceptions
{
    public sealed class CustomerNotFoundException(Guid id)
     : NotFoundExpceptions($"Customer with Id = {id} not found")
    {
    }
}
