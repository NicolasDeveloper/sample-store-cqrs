using System;
using System.Collections.Generic;
using System.Text;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns
{
    public enum EOrderStatus
    {
        Created = 1,
        Paid = 2,
        Shipped = 4,
        Canceled = 5
    }
}
