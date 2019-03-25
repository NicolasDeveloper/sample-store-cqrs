using SampleStoreCQRS.Domain.Core.Commands;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.PaymentProccess.Commands.Inputs
{
    public class PaymentProccessCommand : Command
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
