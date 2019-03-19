using System.Text.RegularExpressions;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class Email : ValueObject<Email>
    {

        public string Address { get; private set; }

        public Email(string address)
        {
            Address = address;
            ValidationResult = new EmailValidation().Validate(this);
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        protected override bool EqualsCore(Email other)
        {
            return Address == other.Address;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = (Address.GetHashCode() * 397) ^ Address.GetHashCode();

                return hashCode;
            }
        }

    }
}
