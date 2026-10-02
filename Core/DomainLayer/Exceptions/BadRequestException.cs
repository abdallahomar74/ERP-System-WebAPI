namespace DomainLayer.Exceptions
{
    public class BadRequestException(List<ValidationError> errors)
     : Exception("Validation Failed")
    {
        public List<ValidationError> Errors { get; } = errors;
    }
}
