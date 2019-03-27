using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class CreditCard : ValueObject<CreditCard>, IPaymentMethod
    {

        public virtual string Number { get; protected set; }
        public virtual int Cvv { get; protected set; }
        public virtual string Validate { get; protected set; }
        public virtual string PrintName { get; protected set; }

        protected CreditCard() {
            ;
        }

        public CreditCard(string number, int cvv, string validate, string printName)
        {
            Number = number;
            Cvv = cvv;
            Validate = validate;
            PrintName = printName;

        }

        public override bool IsValid()
        {
            ValidationResult = new CreditCardValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        
        protected override bool EqualsCore(CreditCard other)
        {
            return Number == other.Number &&
                Cvv == other.Cvv &&
                Validate == other.Validate &&
                PrintName == other.PrintName;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Number.GetHashCode();
                hashCode = (hashCode * 397) ^ Cvv.GetHashCode();
                hashCode = (hashCode * 397) ^ Validate.GetHashCode();
                hashCode = (hashCode * 397) ^ PrintName.GetHashCode();

                return hashCode;
            }
        }
    }
}
