using SampleStoreCQRS.Domain.Core.Interfaces;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.Validations.ValueObjects;

namespace SampleStoreCQRS.Domain.Core.ValueObjects
{
    public class CreditCard : ValueObject<CreditCard>, IPaymentMethod
    {

        public string Number { get; private set; }
        public int Cvv { get; private set; }
        public string Validate { get; private set; }
        public string PrintName { get; private set; }

        protected CreditCard() { }

        public CreditCard(string number, int cvv, string validate, string printName)
        {
            Number = number;
            Cvv = cvv;
            Validate = validate;
            PrintName = printName;

            ValidationResult = new CreditCardValidation().Validate(this);
        }

        public override bool IsValid()
        {
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
