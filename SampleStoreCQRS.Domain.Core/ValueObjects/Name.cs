using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class Name : ValueObject<Name>
    {

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            ValidationResult = new NameValidation().Validate(this);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override bool EqualsCore(Name other)
        {
            return FirstName == other.FirstName &&
                LastName == other.LastName;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = FirstName.GetHashCode();
                hashCode = (hashCode * 397) ^ LastName.GetHashCode();

                return hashCode;
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
