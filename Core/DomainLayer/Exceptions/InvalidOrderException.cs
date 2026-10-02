namespace DomainLayer.Exceptions
{
    public sealed class InvalidOrderException(List<ValidationError> errors)
        : BadRequestException(errors)
    {
    }
}