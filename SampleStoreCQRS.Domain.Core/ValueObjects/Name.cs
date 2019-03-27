using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class Name : ValueObject<Name>
    {

        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }

        protected Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }        

        public override bool IsValid()
        {
            ValidationResult = new NameValidation().Validate(this);
            return ValidationResult.IsValid;
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

        
    }
}
