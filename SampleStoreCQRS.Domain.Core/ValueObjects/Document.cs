using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class Document : ValueObject<Document>
    { 
        public string Number { get; private set; }

        public Document(string number)
        {
            Number = number;
        }

        public override bool IsValid()
        {
            ValidationResult = new DocumentValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        protected override bool EqualsCore(Document other)
        {
            return Number == other.Number;
        }

        public override string ToString()
        {
            return Number;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = (Number.GetHashCode() * 397) ^ Number.GetHashCode();

                return hashCode;
            }
        }
    }
}
