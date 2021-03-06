﻿using System;
using System.Linq;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Interfaces;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System.Collections.Generic;
using SampleStoreCQRS.Tests.Contexts.Common.Repositories;
using SampleStoreCQRS.Domain.Contexts.Promotions.Models;

namespace SampleStoreCQRS.Tests.Contexts.Checkout.Orders.Fakes
{
    public class DiscountCuponFakeReaderRepository : RepositoryFake<Coupon>, IDiscountCuponReaderRepository
    {
        IList<DiscountCupon> _cupons;

        public DiscountCuponFakeReaderRepository()
        {
            _cupons = new List<DiscountCupon>();

            var cupon1 = new DiscountCupon("XPTO1", 5, Period.Monthly);
            var cupon2 = new DiscountCupon("XPTO2", 10, Period.Monthly);
            var cupon3 = new DiscountCupon("XPTO3", 15, Period.Monthly);
            var cupon4 = new DiscountCupon("XPTO4", 20, Period.Monthly);
            var cupon5 = new DiscountCupon("XPTO5", 25, Period.Monthly);
            var cupon6 = new DiscountCupon("XPTO6", 30, Period.Monthly);
            var cupon7 = new DiscountCupon("XPTO8", 5, Period.Create(new DateTime(2008, 12, 25), new DateTime(2008, 12, 26)));

            _cupons.Add(cupon1);
            _cupons.Add(cupon2);
            _cupons.Add(cupon3);
            _cupons.Add(cupon4);
            _cupons.Add(cupon5);
            _cupons.Add(cupon6);
            _cupons.Add(cupon7);
        }

        public DiscountCupon GetCupon(string cod)
        {
            return _cupons.Where(x => x.Cod == cod)?.FirstOrDefault();
        }
    }
}
