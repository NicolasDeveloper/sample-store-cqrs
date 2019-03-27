using System;
using System.Linq;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System.Collections.Generic;
using SampleStoreCQRS.Domain.Core.Interfaces;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Order : Aggregate
    {
        protected IList<OrderItem> _items;

        protected Order() { }

        protected Order(
            Customer customer,
            Payment payment)
        {
            Customer = customer;
            _items = new List<OrderItem>();
            Status = EOrderStatus.Created;
            Payment = payment;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        public virtual IList<OrderItem> Items => _items.ToArray();
        public virtual EOrderStatus Status { get; protected set; }

        public virtual string Number { get; protected set; }
        public virtual DateTime CreateAt { get; protected set; }
        public virtual DateTime UpdateAt { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public virtual Payment Payment { get; protected set; }
        public virtual DiscountCupon DiscountCupon { get; protected set; }
        public virtual decimal Total { get { return _items.Sum(x => x.Price); } }
        public virtual decimal TotalWithDiscount { get; protected set; }

        // add an item
        public void AddItem(Product product, decimal quantity)
        {
            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }

        // place 
        public void Place()
        {

            if (_items.Count == 0)
            {
                AddNotification("Este pedido não possui itens");
                return;
            }

            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount));
            AddEvent(new OrderPlacedEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount, Payment));
        }

        // pay
        public void Pay()
        {
            if (Status == EOrderStatus.Canceled)
            {
                AddNotification("Este pedido não pode ser pago pois está cancelado");
                return;
            }

            Status = EOrderStatus.Paid;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount));
        }

        // ship
        public void Ship()
        {

            if (Status != EOrderStatus.Paid)
            {
                AddNotification("Este pedido não pode ser entregue pois o pagamento não foi processado");
                return;
            }

            if (Status == EOrderStatus.Canceled)
            {
                AddNotification("Este pedido não pode ser entregue pois está cancelado");
                return;
            }

            Status = EOrderStatus.Shipped;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount));
        }

        // canceled
        public void Cancel()
        {

            if (Status == EOrderStatus.Shipped)
            {
                AddNotification("Este pedido não pode ser cancelado pois já foi entregue");
                return;
            }

            Status = EOrderStatus.Canceled;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount));
        }

        // apply discount
        public Order ApplyDiscount(DiscountCupon cupon)
        {
            DiscountCupon = cupon;
            TotalWithDiscount = Total - (Total * (cupon.Percentage / 100));

            AddEvent(new AppliedDiscountEvent(Id, Customer.Id, Number, Status, Total, TotalWithDiscount, DiscountCupon.Cod, DiscountCupon.Percentage));
            
            return this;
        }

        public override bool IsValid()
        {
            ValidationResult = new OrderValidation().Validate(this);

            _items.ToList().ForEach(x =>
            {
                if(!x.IsValid())
                    AddValidationResults(x.ValidationResult);

                AddNotifications(x.Notifications);
            });
            
            return ValidationResult.IsValid && !HasNotifications;
        }

        public static class Factory
        {
            public static Order Create(Customer customer, IPaymentMethod paymentMethod)
            {
                return new Order(customer, new Payment(paymentMethod));
            }
        }
    }
}
