namespace AM.Core.Domain.CQRS.Models
{
    public record FilterModel(string FieldName, string Comparision, string FieldValue);
}
