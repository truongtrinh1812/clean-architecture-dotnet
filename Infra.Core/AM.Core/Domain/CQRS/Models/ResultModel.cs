namespace AM.Core.Domain.CQRS.Models
{
    public record ResultModel<T>(T Data, bool IsError = false, string ErrorMessage = default!) where T : notnull
    {
        public static ResultModel<T> Create(T data, bool isError = false, string errorMessage = default!)
        {
            return new(data, isError, errorMessage);
        }
    }
}
