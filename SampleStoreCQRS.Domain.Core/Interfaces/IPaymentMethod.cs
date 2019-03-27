using FluentValidation.Results;

namespace SampleStoreCQRS.Domain.Core.Interfaces
{
    public interface IPaymentMethod
    {
        ValidationResult ValidationResult { get; }
        bool IsValid();
    }
}
